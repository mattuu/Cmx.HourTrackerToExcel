using AutoMapper;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Import.Models;
using Cmx.HourTrackerToExcel.Mappers.Profiles;
using Cmx.HourTrackerToExcel.TestUtils;
using Ploeh.AutoFixture.Idioms;
using Shouldly;
using Xunit;

namespace Cmx.HourTrackerToExcel.Mappers.Tests
{
    public class CsvLineToWorkDayProfileTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(CsvLineToWorkDayProfile).GetConstructors());
        }

        //[Theory, AutoMoqData]
        public void Map_ShouldReturnCorrectResult(CsvLine csvLine, IMapper sut)
        {
            // act..
            var actual = sut.Map<IWorkDay>(csvLine);

            // assert..
            actual.Date.ShouldBe(csvLine.ClockedIn.Date);
            actual.StartTime.ShouldBe(csvLine.ClockedIn.TimeOfDay);
            actual.EndTime.ShouldBe(csvLine.ClockedOut.TimeOfDay);
            actual.BreakDuration.ShouldBe(-csvLine.TotalTimeAdjustment.Value);
        }
    }
}
