using System;
using System.Collections.Generic;
using System.Linq;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services.Models
{
    public class TimesheetWeek : ITimesheetWeek
    {
        private readonly IDictionary<DayOfWeek, IWorkDay> _days;

        public TimesheetWeek()
        {
            _days = new Dictionary<DayOfWeek, IWorkDay>();
        }

        public bool IsFull => WorkDays?.Length == 7;

        public IWorkDay[] WorkDays
        {
            get
            {
                var days = _days.Values
                                .OrderBy(d => d.Date.DayOfWeek, new WeekDayComparer())
                                .ToList();

                return days.ToArray();
            }
        }

        public void AddDay(IWorkDay workDay)
        {
            if (_days.Count == 7)
            {
                throw new ArgumentException("Attempt on adding more than 7 days to a week");
            }

            if (_days.ContainsKey(workDay.Date.DayOfWeek))
            {
                throw new ArgumentException($"{workDay.Date.DayOfWeek} already present in dictionary");
            }

            _days.Add(workDay.Date.DayOfWeek, workDay);
        }

        private class WeekDayComparer : IComparer<DayOfWeek>
        {
            public int Compare(DayOfWeek x, DayOfWeek y)
            {
                return Shift(x) < Shift(y) ? -1 : 1;
            }

            private int Shift(DayOfWeek dayOfWeek)
            {
                return dayOfWeek == DayOfWeek.Sunday ? 7 : (int) dayOfWeek;
            }
        }
    }
}