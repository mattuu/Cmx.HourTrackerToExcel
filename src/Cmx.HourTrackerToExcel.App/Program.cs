using System;
using System.IO;
using Cmx.HourTrackerToExcel.Import;

namespace Cmx.HourTrackerToExcel.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvReader = new CsvDataReader();
            var path = Path.Combine(Environment.CurrentDirectory, "..", "..", "export.csv");
            using (var stream = File.OpenRead(path))
            {
                var csvLines = csvReader.Read(stream);

                foreach (var item in csvLines)
                {
                    Console.WriteLine(item.ClockedIn.ToString("g"));
                }
            }

            Console.ReadLine();
        }
    }
}
