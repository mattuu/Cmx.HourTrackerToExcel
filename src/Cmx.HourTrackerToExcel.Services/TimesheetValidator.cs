using System;
using System.Threading.Tasks;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public class TimesheetValidator : ITimesheetValidator
    {
        private readonly IWorkedHoursCalculator _workedHoursCalculator;

        public TimesheetValidator(IWorkedHoursCalculator workedHoursCalculator)
        {
            _workedHoursCalculator = workedHoursCalculator ?? throw new ArgumentNullException(nameof(workedHoursCalculator));
        }

        public void AdjustTimesheet(ITimesheet timesheet)
        {
            Parallel.ForEach(timesheet.Weeks,
                             timesheetWeek => Parallel.ForEach(timesheetWeek.WorkDays,
                                                               workDay =>
                                                               {
                                                                   _workedHoursCalculator.AdjustTimes(workDay);
                                                                   _workedHoursCalculator.VerifyTimes(workDay);
                                                               }));
        }
    }
}