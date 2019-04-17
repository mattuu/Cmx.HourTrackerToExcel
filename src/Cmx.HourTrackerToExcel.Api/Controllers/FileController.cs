using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmx.HourTrackerToExcel.Export;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using OfficeOpenXml;

namespace Cmx.HourTrackerToExcel.Api.Controllers
{
    [Route("[controller]")]
    public class FileController : Controller
    {
        private ICsvDataReader _csvDataReader;
        private IMapper _mapper;
        private ITimesheetInitializer _timesheetInitializer;
        private ITimesheetValidator _timesheetValidator;
        private ITimesheetExportManager _timesheetExportManager;
        IFileProvider _fileProvider;

        public FileController(ICsvDataReader csvDataReader,
                            IMapper mapper,
                            ITimesheetInitializer timesheetInitializer,
                            ITimesheetValidator timesheetValidator,
                            ITimesheetExportManager timesheetExportManager,
                            IFileProvider fileProvider)
        {
            _csvDataReader = csvDataReader ?? throw new ArgumentException(nameof(csvDataReader));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _timesheetInitializer = timesheetInitializer ?? throw new ArgumentException(nameof(timesheetInitializer));
            _timesheetValidator = timesheetValidator ?? throw new ArgumentException(nameof(timesheetValidator));
            _timesheetExportManager = timesheetExportManager ?? throw new ArgumentException(nameof(timesheetExportManager));
            _timesheetExportManager = timesheetExportManager ?? throw new ArgumentException(nameof(timesheetExportManager));
            _fileProvider = fileProvider ?? throw new ArgumentException(nameof(fileProvider));
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            var filePath = $@"C:\temp\upload";
            var fileName = "PATH.txt";

            //using (var readStream = new FileStream(filePath, FileMode.Open))
            //{
            //    var memory = new MemoryStream();
            //    await readStream.CopyToAsync(memory);

            return Task.Run(() =>
            {
                IFileInfo fileInfo = _fileProvider.GetFileInfo(fileName);
                var readStream = fileInfo.CreateReadStream();
                var mimeType = "text/plain";

                Response.Headers.Add("Content-Disposition", "attachment");

                return (IActionResult)File(readStream, mimeType, fileName);
            });
            //}
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFileCollection formFiles)
        {
            // full path to file in temp location
            var formFile = formFiles.First();
            var fileName = formFile.FileName;

            if (formFile.Length > 0)
            {
                var destinationFileName =  $"{Guid.NewGuid()}.xlsx";
                var filePath = Path.Combine(Path.GetTempPath(), destinationFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                // using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream);
                }


                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    var csvLines = _csvDataReader.Read(stream);

                    var workDays = csvLines.Select(_mapper.Map<WorkDay>).ToList();

                    var timesheet = _timesheetInitializer.Initialize(workDays);
                    _timesheetValidator.AdjustTimesheet(timesheet);

                    using (var package = new ExcelPackage())
                    {
                        if (package.Workbook.Worksheets.Count == 0)
                        {
                            package.Workbook.Worksheets.Add("Sheet1");
                        }

                        var worksheet = package.Workbook.Worksheets[0];
                        _timesheetExportManager.Export(worksheet, timesheet);

                        package.SaveAs(new FileInfo(filePath));
                    }

                    var fileInfo = _fileProvider.GetFileInfo(destinationFileName);
                    var readStream = fileInfo.CreateReadStream();
                    var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    Response.Headers.Add("Content-Disposition", "attachment");
                    Response.Headers.Add("X-FileName", destinationFileName);

                    return (IActionResult)File(readStream, mimeType, destinationFileName);
                }
            }







            return Ok();
        }
    }

    public class Model
    {
        public IFormFile File { get; set; }
    }
}