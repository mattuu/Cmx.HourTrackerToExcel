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

        public TimesheetDay(IWorkDay workDay)
        {
            Date = workDay.Date;
            StartTime = workDay.StartTime;
            EndTime = workDay.EndTime;
            BreakDuration = workDay.BreakDuration;
            WorkedHours = workDay.WorkedHours;
        }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan BreakDuration { get; set; }

        public TimeSpan WorkedHours { get; set; }
    }
}