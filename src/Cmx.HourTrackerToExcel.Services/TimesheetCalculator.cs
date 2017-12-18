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
        public Task<IEnumerable<ITimesheetWeek>> Aggregate(IEnumerable<IWorkDay> workDays)
        {
            return Task.Run(() =>
            {
                var result = new HashSet<TimesheetWeek>();
                var timesheetWeek = new TimesheetWeek();

                foreach (var workDay in workDays)
                {
                    timesheetWeek.AddDay(workDay);

                    if (workDay.Date.DayOfWeek == DayOfWeek.Sunday)
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
