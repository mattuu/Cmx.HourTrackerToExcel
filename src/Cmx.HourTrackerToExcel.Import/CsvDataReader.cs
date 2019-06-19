using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using CsvHelper;

namespace Cmx.HourTrackerToExcel.Import
{
    public class CsvDataReader : ICsvDataReader
    {
        public IEnumerable<ICsvLine> Read(Stream stream)
        {
            using (TextReader textReader = new StreamReader(stream))
            {
                using (var csv = new CsvReader(textReader))
                {
                    csv.Configuration.HasHeaderRecord = true;

                    csv.Configuration.BadDataFound = context =>
                    {
                        Console.WriteLine($"Bad data found on row '{context.RawRow}'");
                        throw new ApplicationException($"Bad data found on row '{context.RawRow}'");
                    };

                    csv.Configuration.ReadingExceptionOccurred = exception =>
                    {
                        Console.WriteLine($"Reading exception: {exception.Message}");
                        throw new ApplicationException("CSV reading exception", exception);
                    };

                    csv.Configuration.PrepareHeaderForMatch = h => h.Replace(" ", string.Empty).Trim();
                    csv.Configuration.RegisterClassMap<CsvLineMap>();

                    var records = new HashSet<ICsvLine>();

                    while (csv.Read())
                    {
                        var csvLine = csv.GetRecord<CsvLine>();
                        if (csvLine != null)
                        {
                            records.Add(csvLine);
                        }
                    }

                    if (records.Count() == 0)
                    {
                        throw new ApplicationException("Unable to parse uploaded CSV");
                    }

                    return records;
                }
            }
        }
    }
}