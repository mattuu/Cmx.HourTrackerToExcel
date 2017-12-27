using System.Collections.Generic;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Services.Models
{
    public class Timesheet : ITimesheet
    {
        private readonly ICollection<ITimesheetWeek> _weeks = new List<ITimesheetWeek>();

        public IEnumerable<ITimesheetWeek> Weeks => _weeks;

        public void AddWeek(ITimesheetWeek timesheetWeek)
        {
            _weeks.Add(timesheetWeek);
        }
    }
}