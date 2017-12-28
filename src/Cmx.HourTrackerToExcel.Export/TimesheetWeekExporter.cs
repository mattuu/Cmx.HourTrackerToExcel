using System;
using System.Collections.Generic;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using OfficeOpenXml;

namespace Cmx.HourTrackerToExcel.Export
{
    public interface ITimesheetWeekExporter
    {
        void Export(ITimesheetExportManager exportManager, ITimesheetWeek timesheetWeek);
    }

    public class TimesheetWeekExporter : ITimesheetWeekExporter
    {
        internal const string TimeFormat = "hh:mm";

        public void Export(ITimesheetExportManager exportManager, ITimesheetWeek timesheetWeek)
        {
            var addresses = new Dictionary<DateTime?, Tuple<int, int>>();

            exportManager.MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                addresses.Add(workDay.Date, Tuple.Create(exportManager.CurrentColumn, exportManager.CurrentRow));

                exportManager.Write(workDay.Date, d => d.ToString("dd-MMM"))
                             .FontBold()
                             .MoveRight();
            }

            exportManager.NewLine()
                         .Write("In")
                         .FontBold()
                         .MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                if (workDay.OnTimesheet)
                {
                    exportManager.Write(workDay.StartTime.Ticks)
                                 .Format(TimeFormat)
                                 .AlignRight();
                }
                exportManager.MoveRight();
            }

            exportManager.NewLine()
                         .Write("Break")
                         .FontBold()
                         .MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                if (workDay.OnTimesheet)
                {
                    exportManager.Write(workDay.BreakDuration.Ticks)
                                 .Format(TimeFormat)
                                 .AlignRight();
                }
                exportManager.MoveRight();
            }

            exportManager.NewLine()
                         .Write("Out")
                         .FontBold()
                         .MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                if (workDay.OnTimesheet)
                {
                    exportManager.Write(workDay.EndTime.Ticks)
                                 .Format(TimeFormat)
                                 .AlignRight();
                }
                exportManager.MoveRight();
            }

            exportManager.NewLine()
                         .Write("Total")
                         .FontBold()
                         .MoveRight();

            foreach (var workDay in timesheetWeek.WorkDays)
            {
                var startRow = addresses[workDay.Date];
                var startTimeAddress = new ExcelAddress(startRow.Item2 + 1, startRow.Item1, startRow.Item2 + 1, startRow.Item1);
                var breakAddress = new ExcelAddress(startRow.Item2 + 2, startRow.Item1, startRow.Item2 + 2, startRow.Item1);
                var endTimeAddress = new ExcelAddress(startRow.Item2 + 3, startRow.Item1, startRow.Item2 + 3, startRow.Item1);

                var formula = $"={endTimeAddress.Address}-{breakAddress.Address}-{startTimeAddress.Address}";

                exportManager.Formula(formula)
                             .Format(TimeFormat)
                             .AlignRight()
                             .MoveRight();
            }

            exportManager.MoveRight();
            exportManager.Format(TimeFormat);
            exportManager.Formula($"SUM(B{exportManager.CurrentRow}:H{exportManager.CurrentRow})");

            exportManager.NewLine(3);
        }
    }
}
