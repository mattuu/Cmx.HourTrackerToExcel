using System;

namespace Cmx.HourTrackerToExcel.Common.Interfaces
{
    public interface IWorkDay
    {
        DateTime Date { get; set; }

        TimeSpan StartTime { get; set; }

        TimeSpan EndTime { get; set; }

        TimeSpan BreakDuration { get; set; }

        TimeSpan WorkedHours { get; set; }
    }
}
