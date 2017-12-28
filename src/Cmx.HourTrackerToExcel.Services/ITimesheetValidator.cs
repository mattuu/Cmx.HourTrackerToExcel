using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services
{
    public interface ITimesheetValidator
    {
        void AdjustTimesheet(ITimesheet timesheet);
    }
}