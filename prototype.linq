<Query Kind="Program">
  <NuGetReference>EPPlus</NuGetReference>
  <Namespace>OfficeOpenXml</Namespace>
  <Namespace>OfficeOpenXml.Packaging</Namespace>
</Query>

void Main()
{
	var fileName = @"c:\temp\tmp.xlsx";
	using (var package = new ExcelPackage(new FileInfo(fileName)))
	{
		var sheet = package.Workbook.Worksheets["my sheet"];
		if (sheet == null)
			sheet = package.Workbook.Worksheets.Add("my sheet");

		var start = new DateTime(2017, 12, 1);
		var dates = new List<DateTime>();
		var startPadded = start.AddDays(-(int)start.DayOfWeek);
		for (var d = startPadded; d < start.AddMonths(1); d = d.AddDays(1))
		{
			dates.Add(d);
		}


		//		var tw = new TimesheetWeek(new DateTime(2017, 12, 6));
		//		tw.WriteToWorksheet(sheet, 1);

		var month = new TimesheetMonth(start);
		month.Calculate();
		month.Print();


		foreach (var d in dates)
		{
			//			if (d.DayOfWeek == DayOfWeek.Monday)
			//			{
			//				var tw = new TimesheetWeek(d);
			//				tw.WriteToWorksheet(sheet, 1);
			//			}

			//			colIndex = (int)start.DayOfWeek;
			//			if (colIndex == 0)
			//				colIndex = 7;
			//
			//			sheet.Cells[rowIndex, colIndex].Value = d.ToString("dd/MM");
			//			sheet.Cells[rowIndex + 1, colIndex].Value = d.ToString("g");
			//			sheet.Cells[rowIndex + 2, colIndex].Value = "start time";
			//			sheet.Cells[rowIndex + 3, colIndex].Value = "break";
			//			sheet.Cells[rowIndex + 4, colIndex].Value = "end time";
			//
			//			if (colIndex == 7)
			//				rowIndex += 2;
		}

		//
		//		sheet.Cells[1, 1, rowIndex, colIndex].AutoFitColumns();
		//
		//		package.Save();

		Console.WriteLine("Done...");
		//	Process.Start(fileName);
	}
}

public class TimesheetMonth
{
	private ICollection<TimesheetWeek> _weeks = new HashSet<TimesheetWeek>();
	private DateTime _startDate;

	public DateTime MonthStart => _startDate.AddDays(-_startDate.Day);
	public DateTime MonthEnd => MonthStart.AddDays(-1).AddMonths(1);

	public TimesheetMonth(DateTime startDate)
	{
		_startDate = startDate;
	}

	public void Calculate()
	{
		var date = MonthStart;

		if (date.DayOfWeek != DayOfWeek.Monday)
		{
			date = date.AddDays(-(int)date.DayOfWeek);
		}

		while (date <= MonthEnd)
		{
			if (date.DayOfWeek == DayOfWeek.Monday)
			{
				_weeks.Add(new TimesheetWeek(date, this));
			}
			date = date.AddDays(1);
		}
	}

	public void Print()
	{
		foreach (var week in _weeks)
		{
			week.Print();
		}
	}
}

public class TimesheetWeek
{
	private IDictionary<DateTime, bool> _dates = new Dictionary<DateTime, bool>();

	public TimesheetWeek(DateTime startDate, TimesheetMonth month)
	{
		var weekStartDate = startDate;
		if (weekStartDate.DayOfWeek != DayOfWeek.Monday)
		{
			weekStartDate = startDate.AddDays(-(int)startDate.DayOfWeek);
		}

		var dates = new HashSet<DateTime>();
		for (var d = weekStartDate; d < weekStartDate.AddDays(7); d = d.AddDays(1))
		{
			_dates.Add(d, d > month.MonthStart && d < month.MonthEnd);
		}
	}

	public void Print()
	{
		Console.WriteLine(_dates);
	}

	public void WriteToWorksheet(ExcelWorksheet worksheet, int rowIndex)
	{
		Console.WriteLine(_dates);
	}

}
