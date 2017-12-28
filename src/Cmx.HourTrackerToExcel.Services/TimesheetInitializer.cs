using System;
using System.Collections.Generic;
using System.Linq;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Services.Models;

namespace Cmx.HourTrackerToExcel.Services
{
    public class TimesheetInitializer : ITimesheetInitializer
    {
        public ITimesheet Initialize(IEnumerable<IWorkDay> workDays)
        {
            var timesheet = new Timesheet();
            var dates = new List<DateTime>();

            workDays = workDays.ToList();

            var startDate = workDays.Min(wd => wd.Date);
            var endDate1 = workDays.Max(wd => wd.Date);

            var dt = startDate;
            while (dt <= endDate1)
            {
                dates.Add(dt = dt.AddDays(1));
            }

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

                var matchedWorkDay = workDays.SingleOrDefault(wd => wd.Date == date);

                var workDay = matchedWorkDay == null ? new TimesheetDay(date) : new TimesheetDay(matchedWorkDay);
                timesheetWeek.AddDay(workDay);

                date = date.AddDays(1);
            }

            return timesheet;
        }
    }
}