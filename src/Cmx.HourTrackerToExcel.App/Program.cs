using System;
using System.IO;
using System.Linq;
using AutoMapper;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Export;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.Services;
using OfficeOpenXml;
using Unity;

namespace Cmx.HourTrackerToExcel.App
{
    class Program
    {
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "..", "..", "export.csv");
        private static readonly string OutputDir = Path.Combine(Environment.CurrentDirectory, "..\\..\\output");
        private static readonly string OutputPath = Path.Combine(OutputDir, $"{Guid.NewGuid()}.xlsx");

        static void Main(string[] args)
        {
            EnsureOutputDirExists();

            var container = UnityConfig.GetConfiguredContainer();

            var csvReader = container.Resolve<ICsvDataReader>();
            var mapper = container.Resolve<IMapper>();
            var timesheetInitializer = container.Resolve<ITimesheetInitializer>();
            var timesheetCalculator = container.Resolve<ITimesheetValidator>();
            var timesheetExportManager = container.Resolve<ITimesheetExportManager>();


            using (var stream = File.OpenRead(InputPath))
            {
                var csvLines = csvReader.Read(stream);

                var workDays = csvLines.Select(mapper.Map<WorkDay>).ToList();

                var timesheet = timesheetInitializer.Initialize(workDays);
                timesheetCalculator.AdjustTimesheet(timesheet);

                using (var package = new ExcelPackage())
                {
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        package.Workbook.Worksheets.Add("Sheet1");
                    }

                    var worksheet = package.Workbook.Worksheets[0];
                    timesheetExportManager.Export(worksheet, timesheet);

                    
                    var fileInfo = new FileInfo(OutputPath);
                    package.SaveAs(fileInfo);

                    Console.WriteLine("Done...");
                }
            }

            Console.ReadLine();
        }

  
        private static void EnsureOutputDirExists()
        {
            if (!Directory.Exists(OutputDir))
            {
                Directory.CreateDirectory(OutputPath);
            }
        }
    }
}