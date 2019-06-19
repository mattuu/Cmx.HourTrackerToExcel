using CsvHelper.Configuration;

namespace Cmx.HourTrackerToExcel.Import
{
    public class CsvLineMap : ClassMap<CsvLine>
    {
        public CsvLineMap()
        {
            Map(m => m.ClockedIn).TypeConverterOption.Format("dd/MM/yyyy HH:mm");
            Map(m => m.ClockedOut).TypeConverterOption.Format("dd/MM/yyyy HH:mm");
            AutoMap();
        }
    }
}