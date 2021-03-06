using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Import
{
    public class CsvLine : ICsvLine
    {
        public string Job { get; set; }

        public DateTime ClockedIn { get; set; }

        public DateTime ClockedOut { get; set; }

        public TimeSpan Duration { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal Earnings { get; set; }

        public string Comment { get; set; }

        public string Tags { get; set; }

        public string Breaks { get; set; }

        public string Adjustments { get; set; }

        public TimeSpan? TotalTimeAdjustment { get; set; }

        public decimal? TotalEarningsAdjustment { get; set; }
    }
}