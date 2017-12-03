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

//		start = start.AddDays(1 - (int)start.DayOfWeek);


		var month = new TimesheetMonth(start);
		month.Calculate();
		month.Print();

		//		foreach (var d in dates)
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

	public void Print()
	{
		Console.WriteLine(_dates);
	}

	public void WriteToWorksheet(ExcelWorksheet worksheet, int rowIndex)
	{
		Console.WriteLine(_dates);
	}
}

public class TimesheetMonth
{
	private ICollection<TimesheetWeek> _weeks = new HashSet<TimesheetWeek>();
	private DateTime _startDate;

	public DateTime MonthStart => _startDate.AddDays(1 - _startDate.Day);
	public DateTime MonthEnd =>MonthStart.AddMonths(1).Subtract(TimeSpan.FromDays(1));

	public TimesheetMonth(DateTime startDate)
	{
		_startDate = startDate;
		Console.WriteLine(MonthStart);
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

