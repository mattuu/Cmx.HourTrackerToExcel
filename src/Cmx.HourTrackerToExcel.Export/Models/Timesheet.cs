using System;

namespace Cmx.HourTrackerToExcel.Export.Models
{
    public class Timesheet
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        //public void Initialize(IEnumerable<CsvModel> csvModels)
        //{
        //    if (csvModels.Any())
        //    {
        //        StartDate = csvModels.FirstOrDefault().ClockedIn.Date;
        //        EndDate = csvModels.LastOrDefault().ClockedOut.Date;
        //    }

        //    Console.WriteLine(StartDate);
        //    Console.WriteLine(EndDate);
        //}
    }
}
