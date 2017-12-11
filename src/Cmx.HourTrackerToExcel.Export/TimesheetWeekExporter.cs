using System;
using System.Collections.Generic;
using Cmx.HourTrackerToExcel.Export.Models;
using OfficeOpenXml;

namespace Cmx.HourTrackerToExcel.Export
{
    public class TimesheetWeekExporter
    {
        public void Export(TimesheetWeek week, TimesheetMonthExporter monthExporter)
        {
            var addresses = new Dictionary<DateTime?, ExcelAddress>();

            monthExporter.MoveRight();
            foreach (var date in week.Dates)
            {
                if (date.HasValue)
                {
                    var address = monthExporter.Write(date.Value, d => d.ToString("dd-MMM"));
                    addresses.Add(date, address);
                }
                monthExporter.MoveRight();
            }
            monthExporter.NewLine();
            monthExporter.Write("Start");
            foreach (var date in week.Dates)
            {
                if (date.HasValue)
                {
                    monthExporter.Format("HH:mm");

                }
                monthExporter.MoveRight();
            }

            monthExporter.NewLine();
            monthExporter.Write("Break");
            foreach (var date in week.Dates)
            {
                if (date.HasValue)
                {
                    monthExporter.Format("HH:mm");

                }
                monthExporter.MoveRight();
            }

            monthExporter.NewLine();
            monthExporter.Write("End");
            foreach (var date in week.Dates)
            {
                if (date.HasValue)
                {
                    monthExporter.Format("HH:mm");

                }
                monthExporter.MoveRight();
            }

            monthExporter.NewLine();
            monthExporter.MoveRight();

            foreach (var date in week.Dates)
            {
                if (date.HasValue)
                {
                    var startAddress = addresses[date].Start;
                    var startTimeAddress = new ExcelAddress(startAddress.Row + 1, startAddress.Column, startAddress.Row + 1, startAddress.Column);
                    var breakAddress = new ExcelAddress(startAddress.Row + 2, startAddress.Column, startAddress.Row + 2, startAddress.Column);
                    var endTimeAddress = new ExcelAddress(startAddress.Row + 3, startAddress.Column, startAddress.Row + 3, startAddress.Column);

                    var formula = $"={endTimeAddress.Address}-{breakAddress.Address}-{startTimeAddress.Address}";

                    monthExporter.Formula(formula);
                    monthExporter.Format("HH:mm");
                }
                monthExporter.MoveRight();
            }

            monthExporter.NewLine(3);
        }
    }
}
