using System;
using OfficeOpenXml;

namespace Cmx.HourTrackerToExcel.Export.Models
{
    public class TimesheetWeek
    {
        private DateTime?[] _dates = new DateTime?[7];

        public TimesheetWeek(DateTime date, TimesheetMonth month)
        {
            for (var d = date; d < date.AddDays(7); d = d.AddDays(1))
            {
                var index = (int)d.DayOfWeek;
                if (index == 0)
                {
                    index = 7;
                }
                if (month.MonthStart <= d && month.MonthEnd >= d)
                {
                    _dates[index - 1] = d;
                }
            }
        }

        public DateTime?[] Dates => _dates;

        public void Print()
        {
            Console.WriteLine(_dates);
        }

        public void WriteToWorksheet(ExcelWorksheet worksheet, int rowIndex)
        {
            Console.WriteLine(_dates);
        }
    }
}
