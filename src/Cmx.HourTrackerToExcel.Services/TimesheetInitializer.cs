using System;
using System.Collections.Generic;
using System.Linq;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Services.Models;

namespace Cmx.HourTrackerToExcel.Services
{
    public class TimesheetInitializer
    {
        public IEnumerable<ITimesheetWeek> Initialize(IEnumerable<IWorkDay> workDays, DayOfWeek firstDayOfWeek = DayOfWeek.Sunday)
        {
            workDays = workDays.OrderBy(d => d.Date).ToList();

            var date = workDays.Min(wd => wd.Date).AddDays(- (int) firstDayOfWeek);
            var endDate = workDays.Max(wd => wd.Date);

            var result = new HashSet<TimesheetWeek>();
            var timesheetWeek = new TimesheetWeek();

            //var workDayDictionary = workDays.ToDictionary(wd => wd.Date, wd => wd);

            while (date <= endDate)
            {
                var workDay = new TimesheetDay(date);
                timesheetWeek.AddDay(workDay);

                if (workDay.Date.DayOfWeek == firstDayOfWeek)
                {
                    result.Add(timesheetWeek);
                    timesheetWeek = new TimesheetWeek();
                }

                date = date.AddDays(1);
            }

            return result;
        }
    }
}