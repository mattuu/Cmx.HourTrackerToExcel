using System;
using System.IO;
using System.Linq;
using Cmx.HourTrackerToExcel.Import;

namespace Cmx.HourTrackerToExcel.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var csvReader = new CsvDataReader();
            var path = Path.Combine(Environment.CurrentDirectory, "..", "..", "export.csv");
            var csvLines = csvReader.Read(path);


            //Console.WriteLine(csvLines.Count());
            Console.ReadLine();

        }
    }
}
