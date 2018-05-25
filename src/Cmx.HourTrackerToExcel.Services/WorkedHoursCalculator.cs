using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public class WorkedHoursCalculator : IWorkedHoursCalculator
    {
        public void AdjustTimes(IWorkDay workDay)
        {
            workDay.StartTime = RoundMinutesDown(workDay.StartTime);
            workDay.EndTime = RoundMinutesUp(workDay.EndTime);
            workDay.BreakDuration = RoundMinutesUp(workDay.BreakDuration);
        }

        public void VerifyTimes(IWorkDay workDay)
        {
            var timeSpan = RoundMinutesUp(workDay.EndTime).Subtract(RoundMinutesDown(workDay.StartTime));

            if (workDay.BreakDuration != TimeSpan.Zero)
            {
                timeSpan = timeSpan.Subtract(RoundMinutesDown(workDay.BreakDuration));
            }

            if (timeSpan != workDay.WorkedHours)
            {
                throw new ApplicationException($"Provided Duration do not match calculated value for {workDay.Date:d}");
            }
        }

        private static TimeSpan RoundMinutesDown(TimeSpan source) => new TimeSpan(source.Hours, RoundDown(source.Minutes), source.Seconds);

        private static TimeSpan RoundMinutesUp(TimeSpan source) => new TimeSpan(source.Hours, RoundUp(source.Minutes), source.Seconds);

        private static int RoundDown(int value) => value % 10 != default(int) ? value - value % 10 : value;

        private static int RoundUp(int value) => value % 10 != default(int) ? value + 10 - value % 10 : value;
    }
}