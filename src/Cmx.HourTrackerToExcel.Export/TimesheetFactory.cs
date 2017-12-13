using System;
using System.Collections.Generic;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Export.Models;

namespace Cmx.HourTrackerToExcel.Export
{
    public class TimesheetFactory
    {
        public Timesheet Create(IEnumerable<IWorkDay> workDays)
        {
            throw new NotImplementedException();
        }
    }

    public class WeekSplitter
    {
        public IEnumerable<TimesheetWeek> Split(IEnumerable<IWorkDay> workDays, DayOfWeek weekStart)
        {
            throw new NotImplementedException();
        }
    }
}
