using System;
using System.IO;
using System.Text;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Mappers;
using Cmx.HourTrackerToExcel.Models.Export;

namespace Cmx.HourTrackerToExcel.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvReader = new CsvDataReader();
            var path = Path.Combine(Environment.CurrentDirectory, "..", "..", "export.csv");
            var mapper = AutoMapperConfiguration.GetConfiguredMapper(t => Activator.CreateInstance(t));

            using (var stream = File.OpenRead(path))
            {
                var csvLines = csvReader.Read(stream);

                foreach (var csvLine in csvLines)
                {
                    var workDay = mapper.Map<WorkDay>(csvLine);
                    Render(workDay);
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
