using System;

namespace Cmx.HourTrackerToExcel.Common.Interfaces
{
    public interface IWorkDay
    {
        DateTime Date { get; }

        TimeSpan StartTime { get; }

        TimeSpan EndTime { get; }

        TimeSpan BreakDuration { get; }

        TimeSpan? WorkedHours { get; }
    }
}
