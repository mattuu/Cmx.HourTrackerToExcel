using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public class WorkedHoursCalculator : IWorkedHoursCalculator
    {
        public void AdjustTimes(IWorkDay workDay)
        {
            workDay.StartTime = RoundDownToNearestTenthMinute(workDay.StartTime);
            workDay.BreakDuration = RoundDownToNearestTenthMinute(workDay.BreakDuration);
            workDay.EndTime = GetAdjustedEndTime(workDay);
        }

        public void VerifyTimes(IWorkDay workDay)
        {
            var timeSpan = workDay.EndTime.Subtract(workDay.StartTime);

            if (workDay.BreakDuration != TimeSpan.Zero)
            {
                timeSpan = timeSpan.Subtract(workDay.BreakDuration);
            }

            if (timeSpan != workDay.WorkedHours)
            {
                throw new ApplicationException($"Provided Duration do not match calculated value for {workDay.Date:d}");
            }
        }

        private static TimeSpan RoundDownToNearestTenthMinute(TimeSpan timeSpan)
        {
            var modulo10 = timeSpan.Minutes % 10;
            return timeSpan.Add(TimeSpan.FromMinutes(-modulo10));
        }

        private static TimeSpan GetAdjustedEndTime(IWorkDay workDay)
        {
            return workDay.StartTime.Add(workDay.WorkedHours).Add(workDay.BreakDuration);
        }

    }
}