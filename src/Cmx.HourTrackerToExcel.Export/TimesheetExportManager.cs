using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Cmx.HourTrackerToExcel.Export
{
    public interface ITimesheetExportManager
    {
        int CurrentRow { get; }

        int CurrentColumn { get; }

        void Export(ExcelWorksheet excelWorksheet, ITimesheet timesheet);

        ITimesheetExportManager MoveRight(int colCount = 1);

        ITimesheetExportManager MoveLeft(int colCount = 1);

        ITimesheetExportManager MoveDown(int rowCount = 1);

        ITimesheetExportManager MoveUp(int rowCount = 1);

        ITimesheetExportManager NewLine(int spacing = 1);

        ITimesheetExportManager Write<T>(T value, Func<T, string> formatterFunc = null);

        ITimesheetExportManager Format(string format);

        ITimesheetExportManager Formula(string formula);

        ITimesheetExportManager FontBold();

        ITimesheetExportManager FontNormal();

        ITimesheetExportManager AlignRight();
    }

    public class TimesheetExportManager : ITimesheetExportManager
    {
        private readonly ITimesheetWeekExporter _timesheetWeekExporter;
        private ExcelWorksheet _worksheet;

        public int CurrentRow { get; private set; } = 1;

        public int CurrentColumn { get; private set; } = 1;

        public TimesheetExportManager(ITimesheetWeekExporter timesheetWeekExporter)
        {
            _timesheetWeekExporter = timesheetWeekExporter;
        }

        public void Export(ExcelWorksheet excelWorksheet, ITimesheet timesheet)
        {
            _worksheet = excelWorksheet;

            MoveRight();
            for (var i = 1; i <= 7; i++)
            {
                Write(Enum.GetName(typeof(DayOfWeek), i == 7 ? 0 : i))
                    .FontBold()
                    .MoveRight();
            }

            Write("Totals").FontBold();

            NewLine();
            foreach (var week in timesheet.Weeks)
            {
                _timesheetWeekExporter.Export(this, week);
            }

            _worksheet.Cells[1, 1, CurrentRow, CurrentColumn].AutoFitColumns();
        }

        public ITimesheetExportManager MoveRight(int colCount = 1)
        {
            CurrentColumn += colCount;
            return this;
        }

        public ITimesheetExportManager MoveLeft(int colCount = 1)
        {
            CurrentColumn -= colCount;
            return this;
        }

        public ITimesheetExportManager MoveDown(int rowCount = 1)
        {
            CurrentRow += rowCount;
            return this;
        }

        public ITimesheetExportManager MoveUp(int rowCount = 1)
        {
            CurrentRow -= rowCount;
            return this;
        }

        public ITimesheetExportManager NewLine(int spacing = 1)
        {
            CurrentRow += spacing;
            CurrentColumn = 1;
            return this;
        }

        public ITimesheetExportManager Write<T>(T value, Func<T, string> formatterFunc = null)
        {
            var cells = _worksheet.Cells[CurrentRow, CurrentColumn];
            cells.Value = formatterFunc == null ? $"{value}" : formatterFunc(value);

            return this;
        }

        public ITimesheetExportManager Format(string format)
        {
            _worksheet.Cells[CurrentRow, CurrentColumn].Style.Numberformat.Format = format;
            return this;
        }

        public ITimesheetExportManager Formula(string formula)
        {
            _worksheet.Cells[CurrentRow, CurrentColumn].Formula = formula;
            return this;
        }

        public ITimesheetExportManager FontBold()
        {
            _worksheet.Cells[CurrentRow, CurrentColumn].Style.Font.Bold = true;
            return this;
        }

        public ITimesheetExportManager FontNormal()
        {
            _worksheet.Cells[CurrentRow, CurrentColumn].Style.Font.Bold = true;
            return this;
        }

        public ITimesheetExportManager AlignRight()
        {
            _worksheet.Cells[CurrentRow, CurrentColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            return this;
        }
    }
}
