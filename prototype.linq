<Query Kind="Program">
  <NuGetReference>CsvHelper</NuGetReference>
  <NuGetReference>EPPlus</NuGetReference>
  <Namespace>OfficeOpenXml</Namespace>
  <Namespace>OfficeOpenXml.Packaging</Namespace>
  <Namespace>CsvHelper</Namespace>
  <Namespace>CsvHelper.Configuration.Attributes</Namespace>
</Query>

void Main()
{
	var fileReader = new FileReader();
	fileReader.Read(@"C:\temp\export.csv");

	var fileName = @"c:\temp\tmp.xlsx";
	var fileInfo = new FileInfo(fileName);
	if (fileInfo.Exists)
	{
		fileInfo.Delete();
	}

	using (var package = new ExcelPackage(fileInfo))
	{
		var sheet = package.Workbook.Worksheets["my sheet"];
		if (sheet == null)
			sheet = package.Workbook.Worksheets.Add("my sheet");

		var start = new DateTime(2017, 12, 1);

		//		start = start.AddDays(1 - (int)start.DayOfWeek);



		var weekExporter = new TimesheetWeekExporter();
		var exporter = new TimesheetMonthExporter(sheet, weekExporter);

		var month = new TimesheetMonth(start);
		month.Calculate();

		exporter.Export(month);

		//		month.Print();

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
		package.Save();

		Console.WriteLine("Done...");
		//	Process.Start(fileName);
	}
}

public class FileReader
{
	public void Read(string filePath)
	{
		using (TextReader textReader = File.OpenText(filePath))
		{
			using (var csv = new CsvReader(textReader))
			{
				var records = csv.GetRecords<CsvModel>();
				Console.WriteLine(records);
			}
		}
	}
}

public class CsvModel
{
	[Name("Job")]
	public string Job { get; set; }

	[Name("Clocked In")]
	public DateTime ClockedIn { get; set; }

	[Name("Clocked Out")]
	public DateTime ClockedOut { get; set; }

	public TimeSpan Duration { get; set; }

	[Name("Hourly Rate")]
	public decimal HourlRate { get; set; }

	public decimal Earnings { get; set; }

	public string Comment { get; set; }

	public string Tags { get; set; }

	public string Breaks { get; set; }

	public string Adjustments { get; set; }
	
	public TimeSpan? TotalTimeAdjustment { get; set; }
	
	public decimal? TotalEarningsAdjustment { get; set; }
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

public class TimesheetMonth
{
	private ICollection<TimesheetWeek> _weeks = new HashSet<TimesheetWeek>();
	private DateTime _startDate;

	public DateTime MonthStart => _startDate.AddDays(1 - _startDate.Day);
	public DateTime MonthEnd => MonthStart.AddMonths(1).Subtract(TimeSpan.FromDays(1));

	public TimesheetMonth(DateTime startDate)
	{
		_startDate = startDate;
	}

	public IEnumerable<TimesheetWeek> Weeks => _weeks;

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

public class TimesheetMonthExporter
{
	private int _rowIndex = 1;
	private int _colIndex = 1;
	private TimesheetWeekExporter _timesheetWeekExporter;
	private ExcelWorksheet _worksheet;
	private ExcelStyles _styles;

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