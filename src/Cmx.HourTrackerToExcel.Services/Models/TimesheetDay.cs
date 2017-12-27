using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services.Models
{
    public class TimesheetDay : IWorkDay
    {
        public TimesheetDay(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; }

        public TimeSpan StartTime { get; }

        public TimeSpan EndTime { get; }

        public TimeSpan BreakDuration { get; }

        public TimeSpan? WorkedHours { get; set; }
    }
}