using System;
using System.IO;
using System.Linq;
using AutoMapper;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.Services;
using Unity;

namespace Cmx.HourTrackerToExcel.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = UnityConfig.GetConfiguredContainer();

            var csvReader = container.Resolve<ICsvDataReader>();
            var mapper = container.Resolve<IMapper>();
            var timesheetInitializer = container.Resolve<ITimesheetInitializer>();
            var timesheetCalculator = container.Resolve<ITimesheetValidator>();

            var path = Path.Combine(Environment.CurrentDirectory, "..", "..", "export.csv");

            using (var stream = File.OpenRead(path))
            {
                var csvLines = csvReader.Read(stream);

                var workDays = csvLines.Select(mapper.Map<WorkDay>).ToList();

                var timesheet = timesheetInitializer.Initialize(workDays);
                timesheetCalculator.AdjustTimesheet(timesheet);

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