using AutoMapper;
using Cmx.HourTrackerToExcel.Mappers.Profiles;
using Cmx.HourTrackerToExcel.Mappers.Tests.Infrastructure;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.Models.Import;
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

        //AutoMapperConfiguration.GetConfiguredMapper(t => fixture.Create(t, new SpecimenContext(fixture)))
        [Theory, AutoMapperMoqData]
        public void Map_ShouldReturnCorrectResult(CsvLine csvLine, IMapper sut)
        {
            // act..
            var actual = sut.Map<WorkDay>(csvLine);

            // assert..
            actual.Date.ShouldBe(csvLine.ClockedIn.Date);
            actual.StartTime.ShouldBe(csvLine.ClockedIn.TimeOfDay);
            actual.EndTime.ShouldBe(csvLine.ClockedOut.TimeOfDay);
            actual.BreakDuration.ShouldBe(-csvLine.TotalTimeAdjustment.Value);
        }
    }
}
