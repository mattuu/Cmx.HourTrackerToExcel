using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public class WorkedHoursCalculator : IWorkedHoursCalculator
    {
        internal const int MinutesCutOff = 4;

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

        private static TimeSpan RoundMinutesDown(TimeSpan source) => new TimeSpan(source.Hours, RoundDownToNearestTenth(source.Minutes), source.Seconds);

        private static TimeSpan RoundMinutesUp(TimeSpan source) => new TimeSpan(source.Hours, RoundUpToNearestTenth(source.Minutes), source.Seconds);

        private static int RoundDownToNearestTenth(int value)
        {
            if (value % 10 == 0)
            {
                return value;
            }

            return value % 10 < MinutesCutOff ? value - value % 10 : value + 10 - value % 10;
        }

        private static int RoundUpToNearestTenth(int value)
        {
            if (value % 10 == 0)
            {
                return value;
            }

            return value % 10 < MinutesCutOff ? value + 10 - value % 10 : value - value % 10;
        }
    }
}