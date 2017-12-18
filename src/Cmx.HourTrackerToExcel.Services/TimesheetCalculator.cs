using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Services.Models;

namespace Cmx.HourTrackerToExcel.Services
{
    public class TimesheetCalculator
    {
        public Task<IEnumerable<ITimesheetWeek>> Aggregate(IEnumerable<IWorkDay> workDays, DayOfWeek dayOfWeek = DayOfWeek.Sunday)
        {
            return Task.Run(() =>
            {
                var result = new HashSet<TimesheetWeek>();
                var timesheetWeek = new TimesheetWeek();

                foreach (var workDay in workDays.OrderBy(wd => wd.Date))
                {
                    timesheetWeek.AddDay(workDay);

                    if (workDay.Date.DayOfWeek == dayOfWeek)
                    {
                        result.Add(timesheetWeek);
                        timesheetWeek = new TimesheetWeek();
                    }
                }

                result.Add(timesheetWeek);

                return result.Cast<ITimesheetWeek>();
            });
        }
    }
}
