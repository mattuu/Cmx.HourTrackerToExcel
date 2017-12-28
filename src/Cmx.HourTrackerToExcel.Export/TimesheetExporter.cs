using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Export.Infrastructure;

namespace Cmx.HourTrackerToExcel.Export
{
    public interface ITimesheetExporter
    {
        byte[] Export(ITimesheetExportManager timesheetExportManager, ITimesheet timesheet);
    }

    public class TimesheetExporter : ITimesheetExporter
    {
        private readonly ITimesheetWeekExporter _timesheetWeekExporter;

        public TimesheetExporter(ITimesheetWeekExporter timesheetWeekExporter)
        {
            _timesheetWeekExporter = timesheetWeekExporter ?? throw new ArgumentNullException(nameof(timesheetWeekExporter));
        }

        public byte[] Export(ITimesheetExportManager timesheetExportManager, ITimesheet timesheet)
        {
            throw new NotImplementedException();
        }
    }
}