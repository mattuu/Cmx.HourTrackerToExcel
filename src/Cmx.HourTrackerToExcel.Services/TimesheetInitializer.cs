using System;
using System.Collections.Generic;
using System.Linq;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Services.Models;

namespace Cmx.HourTrackerToExcel.Services
{
    public class TimesheetInitializer
    {
        public ITimesheet Initialize(IEnumerable<IWorkDay> workDays)
        {
            var timesheet = new Timesheet();
            var dates = workDays.Select(wd => wd.Date).ToList();

            while (dates.Min().DayOfWeek != DayOfWeek.Monday)
            {
                dates.Insert(0, dates.Min().AddDays(-1));
            }

            while (dates.Max().DayOfWeek != DayOfWeek.Sunday)
            {
                dates.Add(dates.Max().AddDays(1));
            }

            var date = dates.Min();
            var endDate = dates.Max();

            var timesheetWeek = new TimesheetWeek();
            timesheet.AddWeek(timesheetWeek);

            while (date <= endDate)
            {
                if (timesheetWeek.IsFull)
                {
                    timesheetWeek = new TimesheetWeek();
                    timesheet.AddWeek(timesheetWeek);
                }

                var workDay = new TimesheetDay(date);
                timesheetWeek.AddDay(workDay);

                date = date.AddDays(1);
            }

            return timesheet;
        }
    }
}