using System;
using System.Collections.Generic;
using System.Text;
using Cmx.HourTrackerToExcel.Export;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.Services;
using Microsoft.AspNetCore.Http;

namespace Cmx.HourTrackerToExcel.Services
{
    public class CsvToTimesheetConverter
    {
        private readonly ICsvDataReader _csvDataReader;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        private readonly ITimesheetExportManager _timesheetExportManager;
        private readonly ITimesheetInitializer _timesheetInitializer;
        private readonly ITimesheetValidator _timesheetValidator;

        public CsvToTimesheetConverter(ICsvDataReader csvDataReader,
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
            _timesheetExportManager =
                timesheetExportManager ?? throw new ArgumentException(nameof(timesheetExportManager));
            _timesheetExportManager =
                timesheetExportManager ?? throw new ArgumentException(nameof(timesheetExportManager));
            _fileProvider = fileProvider ?? throw new ArgumentException(nameof(fileProvider));
        }

        public async Task<string> Convert(IFormFile formFile)
        {
            // full path to file in temp location
            var fileName = formFile.FileName;

            if (formFile.Length > 0)
            {
                var destinationFileName = $"{Guid.NewGuid()}.xlsx";
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
                        if (package.Workbook.Worksheets.Count == 0) package.Workbook.Worksheets.Add("Sheet1");

                        var worksheet = package.Workbook.Worksheets[0];
                        _timesheetExportManager.Export(worksheet, timesheet);

                        package.SaveAs(new FileInfo(filePath));
                        return filePath;
                    }

                    //var fileInfo = _fileProvider.GetFileInfo(destinationFileName);
                    //var readStream = fileInfo.CreateReadStream();
                    //var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    //Response.Headers.Add("Content-Disposition", "attachment");
                    //Response.Headers.Add("X-FileName", destinationFileName);

                    //return File(readStream, mimeType, destinationFileName);
                }
            }


        }
    }
}
