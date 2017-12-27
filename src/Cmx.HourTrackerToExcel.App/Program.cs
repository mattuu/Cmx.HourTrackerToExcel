using System;
using System.IO;
using System.Linq;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Mappers;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.Services;

namespace Cmx.HourTrackerToExcel.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvReader = new CsvDataReader();
            var path = Path.Combine(Environment.CurrentDirectory, "..", "..", "export.csv");
            var mapper = AutoMapperConfiguration.GetConfiguredMapper(Activator.CreateInstance);

            var timesheetInitializer = new TimesheetInitializer();

            using (var stream = File.OpenRead(path))
            {
                var csvLines = csvReader.Read(stream);

                var workDays = csvLines.Select(mapper.Map<WorkDay>);

                var timesheet =  timesheetInitializer.Initialize(workDays);

                foreach (var week in timesheet.Weeks)
                {
                    Console.WriteLine("week");
                    foreach (var workDay in week.WorkDays)
                    {
                        Console.WriteLine(Render(workDay));
                    }
                }
            }

            Console.ReadLine();
        }

        private static string Render(IWorkDay workDay)
        {
            return $"{workDay.Date:dd/MM/yyyy} | {workDay.StartTime} | {workDay.EndTime} | {workDay.BreakDuration}";
        }
    }
}
