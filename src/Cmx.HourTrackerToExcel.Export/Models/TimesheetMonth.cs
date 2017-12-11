using System;
using System.Collections.Generic;

namespace Cmx.HourTrackerToExcel.Export.Models
{
    public class TimesheetMonth
    {
        private ICollection<TimesheetWeek> _weeks = new HashSet<TimesheetWeek>();
        private DateTime _startDate;

        public DateTime MonthStart => _startDate.AddDays(1 - _startDate.Day);
        public DateTime MonthEnd => MonthStart.AddMonths(1).Subtract(TimeSpan.FromDays(1));

        public TimesheetMonth(DateTime startDate)
        {
            _startDate = startDate;
        }

        public IEnumerable<TimesheetWeek> Weeks => _weeks;

        public void Calculate()
        {
            var date = MonthStart;

            if (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-(int)date.DayOfWeek);
            }

            while (date <= MonthEnd)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    _weeks.Add(new TimesheetWeek(date, this));
                }
                date = date.AddDays(1);
            }
        }

        public void Print()
        {
            foreach (var week in _weeks)
            {
                week.Print();
            }
        }
    }
}
