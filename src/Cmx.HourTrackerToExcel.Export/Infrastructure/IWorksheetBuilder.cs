using OfficeOpenXml;

namespace Cmx.HourTrackerToExcel.Export.Infrastructure
{
     public interface IWorksheetBuilder
    {
        int ColCount { get; }

        int RowCount { get; }

        WorksheetBuilder AppendHorizontal(object value, string format = null);

        WorksheetBuilder MoveToNewRow();

        WorksheetBuilder AddAutoFilter(ExcelAddress excelAddress);

        WorksheetBuilder AdjustColumnWidth(ExcelAddress excelAddress);
    }

}