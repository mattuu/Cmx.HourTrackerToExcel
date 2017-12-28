using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Models.Export
{
    public class WorkDay : IWorkDay
    {
        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan BreakDuration { get; set; }

        public TimeSpan WorkedHours { get; set; }

        public bool OnTimesheet { get; set; }
    }
}
