using System;
using System.IO;
using System.Linq;
using System.Text;
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

            var timesheetCalculator = new TimesheetCalculator();

            using (var stream = File.OpenRead(path))
            {
                var csvLines = csvReader.Read(stream);

                var workDays = csvLines.Select(mapper.Map<WorkDay>);

                //foreach (var csvLine in csvLines)
                //{
                //    var workDay = mapper.Map<WorkDay>(csvLine);
                //    Render(workDay);

                    
                //}

                var weeks = timesheetCalculator.Calculate(workDays);

                foreach (var week in weeks.Result)
                {
                    Console.WriteLine("week");
                    foreach (var workDay in week.WorkDays)
                    {
                        Console.WriteLine($"{workDay?.Date:d}");
                    }
                }
            }

            Console.ReadLine();
        }

        private static void Render(WorkDay workDay)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{workDay.Date:dd/MM/yyyy}");
            stringBuilder.Append($" | {workDay.StartTime}");
            stringBuilder.Append($" | {workDay.EndTime}");
            stringBuilder.Append($" | {workDay.BreakDuration}");

            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
