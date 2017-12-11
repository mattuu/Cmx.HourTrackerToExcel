using System;
using System.Collections.Generic;
using System.IO;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Import.Models;
using CsvHelper;

namespace Cmx.HourTrackerToExcel.Import
{
    public class CsvDataReader
    {
        public IEnumerable<ICsvLine> Read(string filePath)
        {
            var config = new CsvHelper.Configuration.Configuration
            {
                
            };

            using (var textReader = File.OpenText(filePath))
            {
                using (var csv = new CsvReader(textReader))
                {
                    csv.Configuration.HasHeaderRecord = true;

                    csv.Configuration.BadDataFound = context =>
                    {
                        Console.WriteLine($"Bad data found on row '{context.RawRow}'");
                    };

                    csv.Configuration.ReadingExceptionOccurred = exception =>
                    {
                        Console.WriteLine($"Reading exception: {exception.Message}");
                    };

                    csv.Configuration.PrepareHeaderForMatch = h => h.Replace(" ", string.Empty).Trim();

                    var records = new HashSet<ICsvLine>();

                    while (csv.Read())
                    {
                        records.Add(csv.GetRecord<CsvLine>());
                    }

                    return records;
                }
            }
        }
    }
}