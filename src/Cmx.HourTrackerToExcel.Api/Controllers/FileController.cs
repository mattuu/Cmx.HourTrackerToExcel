using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Models.Export;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;

namespace Cmx.HourTrackerToExcel.Api.Controllers
{
    [Route("[controller]")]
    public class FileController : Controller
    {
        private ICsvDataReader _csvDataReader;
        private IMapper _mapper;

        public FileController(ICsvDataReader csvDataReader, IMapper mapper)
        {
            _csvDataReader = csvDataReader ?? throw new ArgumentException(nameof(csvDataReader));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
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

                IFileProvider provider = new PhysicalFileProvider(filePath);
                IFileInfo fileInfo = provider.GetFileInfo(fileName);
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
                var filePath = Path.GetTempFileName();

                using (var stream = new FileStream(filePath, FileMode.Open))
                // using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream);
                }


                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    var csvLines = _csvDataReader.Read(stream);

                    var workDays = csvLines.Select(_mapper.Map<WorkDay>).ToList();
                    return Ok(workDays.Count());

                    // var timesheet = timesheetInitializer.Initialize(workDays);
                    // timesheetCalculator.AdjustTimesheet(timesheet);

                    // using (var package = new ExcelPackage())
                    // {
                    //     if (package.Workbook.Worksheets.Count == 0)
                    //     {
                    //         package.Workbook.Worksheets.Add("Sheet1");
                    //     }

                    //     var worksheet = package.Workbook.Worksheets[0];
                    //     timesheetExportManager.Export(worksheet, timesheet);


                    //     var fileInfo = new FileInfo(OutputPath);
                    //     package.SaveAs(fileInfo);

                    //     Console.WriteLine("Done...");
                    // }
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