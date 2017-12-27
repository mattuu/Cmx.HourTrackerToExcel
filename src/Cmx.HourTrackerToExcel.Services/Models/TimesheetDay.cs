using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services.Models
{
    public class TimesheetDay : IWorkDay
    {
        public TimesheetDay(DateTime date, TimeSpan? startTime, TimeSpan? endTime, TimeSpan? breakDuration)
        {
            Date = date;
            StartTime = startTime.GetValueOrDefault();
            EndTime = endTime.GetValueOrDefault();
            BreakDuration = breakDuration.GetValueOrDefault();
        }

        public DateTime Date { get; }

        public TimeSpan StartTime { get; }

        public TimeSpan EndTime { get; }

        public TimeSpan BreakDuration { get; }
    }
}