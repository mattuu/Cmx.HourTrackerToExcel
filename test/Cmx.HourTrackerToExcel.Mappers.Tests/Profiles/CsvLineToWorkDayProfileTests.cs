using AutoMapper;
using Cmx.HourTrackerToExcel.Mappers.Profiles;
using Cmx.HourTrackerToExcel.Mappers.Tests.Infrastructure;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using AutoFixture.Idioms;
using Shouldly;
using Xunit;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using System;

namespace Cmx.HourTrackerToExcel.Mappers.Tests.Profiles
{
    public class CsvLineToWorkDayProfileTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(CsvLineToWorkDayProfile).GetConstructors());
        }

        [Theory, AutoMapperMoqData]
        public void Map_ShouldReturnCorrectResult(TestCsvLine csvLine, IMapper sut)
        {
            // act..
            var actual = sut.Map<WorkDay>(csvLine);

            // assert..
            actual.Date.ShouldBe(csvLine.ClockedIn.Date);
            actual.StartTime.ShouldBe(csvLine.ClockedIn.TimeOfDay);
            actual.EndTime.ShouldBe(csvLine.ClockedOut.TimeOfDay);
            actual.BreakDuration.ShouldBe(-csvLine.TotalTimeAdjustment.Value);
            actual.WorkedHours.ShouldBe(csvLine.Duration);
        }

        public class TestCsvLine : ICsvLine
        {
            public string Job { get; set; }

            public DateTime ClockedIn { get; set; }

            public DateTime ClockedOut { get; set; }

            public TimeSpan Duration { get; set; }

            public decimal HourlyRate { get; set; }

            public decimal Earnings { get; set; }

            public string Comment { get; set; }

            public string Tags { get; set; }

            public string Breaks { get; set; }

            public string Adjustments { get; set; }

            public TimeSpan? TotalTimeAdjustment { get; set; }

            public decimal? TotalEarningsAdjustment { get; set; }
        }
    }
}
