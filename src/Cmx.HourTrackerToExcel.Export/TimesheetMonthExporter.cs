using System;
using Cmx.HourTrackerToExcel.Export.Models;
using OfficeOpenXml;

namespace Cmx.HourTrackerToExcel.Export
{
    public class TimesheetMonthExporter
    {
        private int _rowIndex = 1;
        private int _colIndex = 1;
        private TimesheetWeekExporter _timesheetWeekExporter;
        private ExcelWorksheet _worksheet;

        public TimesheetMonthExporter(ExcelWorksheet worksheet, TimesheetWeekExporter timesheetWeekExporter)
        {
            _worksheet = worksheet;
            _timesheetWeekExporter = timesheetWeekExporter;
        }

        public void Export(TimesheetMonth timesheetMonth)
        {
            MoveRight();
            for (var i = 1; i <= 7; i++)
            {
                Write(Enum.GetName(typeof(DayOfWeek), i == 7 ? 0 : i));
                MoveRight();
            }

            NewLine();
            foreach (var week in timesheetMonth.Weeks)
            {
                _timesheetWeekExporter.Export(week, this);
            }

            _worksheet.Cells[1, 1, _rowIndex, _colIndex].AutoFitColumns();
        }

        public void MoveRight(int colCount = 1)
        {
            _colIndex += colCount;
        }

        public void MoveLeft(int colCount = 1)
        {
            _colIndex -= colCount;
        }

        public void MoveDown(int rowCount = 1)
        {
            _rowIndex += rowCount;
        }

        public void MoveUp(int rowCount = 1)
        {
            _rowIndex -= rowCount;
        }

        public void NewLine(int spacing = 1)
        {
            _rowIndex += spacing;
            _colIndex = 1;
        }

        public ExcelAddress Write<T>(T value, Func<T, string> formatterFunc = null)
        {
            var cells = _worksheet.Cells[_rowIndex, _colIndex];
            cells.Value = formatterFunc == null ? $"{value}" : formatterFunc(value);
            return cells;
        }

        public void Format(string format)
        {
            _worksheet.Cells[_rowIndex, _colIndex].Style.Numberformat.Format = format;
        }

        public void Formula(string formula)
        {
            _worksheet.Cells[_rowIndex, _colIndex].Formula = formula;
        }
    }
}
