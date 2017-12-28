using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using OfficeOpenXml;

namespace Cmx.HourTrackerToExcel.Export.Infrastructure
{
    public interface IExcelWorksheetFactory
    {
        ExcelWorksheet Create();
    }

    public class ExcelWorksheetFactory : IExcelWorksheetFactory
    {
        public ExcelWorksheet Create()
        {
            var package = new ExcelPackage();
            return package.Workbook.Worksheets[0];
        }
    }
}
