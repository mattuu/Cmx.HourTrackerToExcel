using System;

namespace Cmx.HourTrackerToExcel.Common.Interfaces
{
    public interface ICsvLine
    {
        string Job { get; }

        DateTime ClockedIn { get; }

        DateTime ClockedOut { get; }

        TimeSpan Duration { get; }

        decimal HourlyRate { get; }

        decimal Earnings { get; }

        string Comment { get; }

        string Tags { get; }

        string Breaks { get; }

        string Adjustments { get; }

        TimeSpan? TotalTimeAdjustment { get; }

        decimal? TotalEarningsAdjustment { get; }
    }
}
