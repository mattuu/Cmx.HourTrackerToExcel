using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using OfficeOpenXml;

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

        ITimesheetExportManager Value<T>(T value, Func<T, object> formatterFunc = null);

        ITimesheetExportManager Format(string format);

        ITimesheetExportManager Formula(string formula, bool calculate = false);

        ITimesheetExportManager FontBold();

        ITimesheetExportManager FontNormal();

        ITimesheetExportManager AlignRight();
    }
}
