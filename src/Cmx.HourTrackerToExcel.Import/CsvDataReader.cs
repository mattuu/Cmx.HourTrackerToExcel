using System.Collections.Generic;
using System.IO;
using Cmx.HourTrackerToExcel.Import.Models;
using CsvHelper;

namespace Cmx.HourTrackerToExcel.Import
{
    public class CsvDataReader
    {
        public IEnumerable<CsvLine> Read(string filePath)
        {
            using (TextReader textReader = File.OpenText(filePath))
            {
                using (var csv = new CsvReader(textReader))
                {
                    var records = csv.GetRecords<CsvLine>();
                    return records;
                }
            }
        }
    }
}