<Query Kind="Statements">
  <NuGetReference>EPPlus</NuGetReference>
  <Namespace>OfficeOpenXml</Namespace>
  <Namespace>OfficeOpenXml.Packaging</Namespace>
</Query>

var fileName = @"c:\temp\tmp.xlsx";
using (var package = new ExcelPackage(new FileInfo(fileName)))
{
	var sheet = package.Workbook.Worksheets["my sheet"];
	if (sheet == null)
		sheet = package.Workbook.Worksheets.Add("my sheet");

	var start = new DateTime(2017, 12, 1);
	var dates = new List<DateTime>();
	for (var d = start; d < start.AddMonths(1); d = d.AddDays(1))
	{
		dates.Add(d);
	}

	var rowIndex = 1;
	var colIndex = (int)start.DayOfWeek;
	if (colIndex == 0)
		colIndex = 7;

	foreach (var d in dates)
	{
		colIndex = (int)start.DayOfWeek;
		if (colIndex == 0)
			colIndex = 7;

		sheet.Cells[rowIndex, colIndex].Value = d.ToString("dd/MM");
		sheet.Cells[rowIndex + 1, colIndex].Value = d.ToString("g");
		sheet.Cells[rowIndex + 2, colIndex].Value = "start time";
		sheet.Cells[rowIndex + 3, colIndex].Value = "break";
		sheet.Cells[rowIndex + 4, colIndex].Value = "end time";

		if (colIndex == 7)
			rowIndex += 2;
	}

	
	sheet.Cells[1, 1, rowIndex, colIndex].AutoFitColumns();

	package.Save();

	Console.WriteLine("Done...");
	//	Process.Start(fileName);
}