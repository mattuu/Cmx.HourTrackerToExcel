using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Export
{
    public interface ITimesheetWeekExporter
    {
        void Export(ITimesheetExportManager exportManager, ITimesheetWeek timesheetWeek);
    }
}