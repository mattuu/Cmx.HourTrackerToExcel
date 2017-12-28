using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public class WorkedHoursCalculator : IWorkedHoursCalculator
    {
        public TimeSpan Calculate(IWorkDay workDay)
        {
            if (workDay.StartTime == TimeSpan.Zero)
                throw new ArgumentException("StartTime cannot be zero");

            if (workDay.EndTime == TimeSpan.Zero)
                throw new ArgumentException("EndTime cannot be zero");

            var timeSpan = RoundMinutesUp(workDay.EndTime).Subtract(RoundMinutesDown(workDay.StartTime));

            if (workDay.BreakDuration != TimeSpan.Zero)
            {
                timeSpan = timeSpan.Subtract(RoundMinutesDown(workDay.BreakDuration));
            }

            return timeSpan;
        }

        internal static TimeSpan RoundMinutesDown(TimeSpan source) => new TimeSpan(source.Hours, RoundDown(source.Minutes), source.Seconds);

        internal static TimeSpan RoundMinutesUp(TimeSpan source) => new TimeSpan(source.Hours, RoundUp(source.Minutes), source.Seconds);

        private static int RoundDown(int value) => value % 10 != default(int) ? value - value % 10 : value;

        private static int RoundUp(int value) => value % 10 != default(int) ? value + 10 - value % 10 : value;
    }
}