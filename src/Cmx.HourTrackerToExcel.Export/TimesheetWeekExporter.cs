using System;
using System.Collections.Generic;
using System.Linq;
using Cmx.HourTrackerToExcel.Common.Interfaces;

namespace Cmx.HourTrackerToExcel.Export
{
    public class TimesheetWeekExporter : ITimesheetWeekExporter
    {
        public void Export(ITimesheetExportManager exportManager, ITimesheetWeek timesheetWeek)
        {
            var addresses = new Dictionary<DateTime?, Tuple<int, int>>();

            exportManager.MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                addresses.Add(workDay.Date, Tuple.Create(exportManager.CurrentColumn, exportManager.CurrentRow));

                exportManager.Value(workDay.Date, d => $"{d:dd-MMM}")
                             .FontBold()
                             .MoveRight();
            }

            exportManager.NewLine()
                         .Value("In")
                         .FontBold()
                         .MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                if (workDay.OnTimesheet)
                {
                    exportManager.Value(workDay.StartTime)
                                 .Format(Constants.TimeFormat)
                                 .AlignRight();
                }
                exportManager.MoveRight();
            }

            exportManager.NewLine()
                         .Value("Break")
                         .FontBold()
                         .MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                if (workDay.OnTimesheet)
                {
                    exportManager.Value(workDay.BreakDuration)
                                 .Format(Constants.TimeFormat)
                                 .AlignRight();
                }
                exportManager.MoveRight();
            }

            exportManager.NewLine()
                         .Value("Out")
                         .FontBold()
                         .MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                if (workDay.OnTimesheet)
                {
                    exportManager.Value(workDay.EndTime)
                                 .Format(Constants.TimeFormat)
                                 .AlignRight();
                }
                exportManager.MoveRight();
            }

            exportManager.NewLine()
                         .Value("Total")
                         .FontBold()
                         .MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                exportManager.Value(workDay.WorkedHours)
                             .Format(Constants.TimeFormat)
                             .AlignRight()
                             .MoveRight();
            }

            var totalWorkedHours = timesheetWeek.WorkDays.Select(wd => wd.WorkedHours).Aggregate((total, ts) => total.Add(ts));
            exportManager.Format(Constants.TimeFormat)
                         .Formula($"SUM(B{exportManager.CurrentRow}:H{exportManager.CurrentRow})")
                         .Value(totalWorkedHours.Duration())
                         .MoveRight();

            exportManager.NewLine(3);
        }
    }
}