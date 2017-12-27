using System.Collections.Generic;

namespace Cmx.HourTrackerToExcel.Common.Interfaces
{
    public interface ITimesheet
    {
        IEnumerable<ITimesheetWeek> Weeks { get; }
    }
}