using System;
using OfficeOpenXml;

namespace Cmx.HourTrackerToExcel.Export.Infrastructure
{
    public class WorksheetBuilder : IWorksheetBuilder
    {
        private readonly ExcelWorksheet _sheet;
        private ExcelCellAddress _internalCellAddress;

        public WorksheetBuilder(ExcelWorksheet sheet)
            : this(sheet, new ExcelCellAddress("A1"))
        {
        }

        public WorksheetBuilder()
        {
            _internalCellAddress = new ExcelCellAddress();
            ColCount = RowCount = 1;
        }

        private WorksheetBuilder(ExcelWorksheet sheet, ExcelCellAddress initialCellCellAddress)
            : this()
        {
            if (sheet == null) throw new ArgumentNullException(nameof(sheet));
            if (initialCellCellAddress == null) throw new ArgumentNullException(nameof(initialCellCellAddress));
            _sheet = sheet;
            _internalCellAddress = initialCellCellAddress;
        }

        public int ColCount { get; private set; }

        public int RowCount { get; private set; }

        public WorksheetBuilder AppendHorizontal(object value, string format = null)
        {
            if (value != null && _sheet != null)
            {
                _sheet.Cells[_internalCellAddress.Address].Value = string.IsNullOrEmpty(format)
                    ? value
                    : string.Format(null, format, value);
            }

            ColCount = _internalCellAddress.Column + 1;
            _internalCellAddress = new ExcelCellAddress(_internalCellAddress.Row, ColCount);
            return this;
        }

        public WorksheetBuilder MoveToNewRow()
        {
            RowCount = _internalCellAddress.Row + 1;
            _internalCellAddress = new ExcelCellAddress(RowCount, 1);
            return this;
        }

        public WorksheetBuilder AddAutoFilter(ExcelAddress excelAddress)
        {
            Apply(excelAddress, range => range.AutoFilter = true);
            return this;
        }

        public WorksheetBuilder AdjustColumnWidth(ExcelAddress excelAddress)
        {
            Apply(excelAddress, range => range.AutoFitColumns());
            return this;
        }

        private void Apply(ExcelAddress address, Action<ExcelRange> actor)
        {
            _sheet.Select(address);
            actor(_sheet.SelectedRange);
            _sheet.Select("A1");
        }
    }
}