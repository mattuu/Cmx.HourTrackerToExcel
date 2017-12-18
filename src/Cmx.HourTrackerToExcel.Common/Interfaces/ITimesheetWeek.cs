namespace Cmx.HourTrackerToExcel.Common.Interfaces
{
    public interface ITimesheetWeek
    {
        IWorkDay[] WorkDays { get; }
    }
}