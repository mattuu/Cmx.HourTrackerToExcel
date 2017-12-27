using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public interface ITimesheetCalculator
    {
        void CalculateWorkingHours(ITimesheet timesheet);
    }
}