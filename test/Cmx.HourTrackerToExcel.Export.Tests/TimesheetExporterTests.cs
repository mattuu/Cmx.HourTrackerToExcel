using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Export.Infrastructure;
using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using Moq;
using Xunit;

namespace Cmx.HourTrackerToExcel.Export.Tests
{
    public class TimesheetExporterTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(TimesheetExporter).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void Export_ShouldReturnCorrectResult(TestTimesheet timesheet, [Frozen] Mock<ITimesheetWeekExporter> worksheetBuilderMock, TimesheetExporter sut)
        {
            // arrange..
            //worksheetBuilderMock.Setup(m => m.)

            // act..
            //var actual = sut.Export(TODO, timesheet);

            // assert..
        }

        public class TestTimesheet : ITimesheet
        {
            private readonly IFixture _fixture;

            public TestTimesheet(IFixture fixture)
            {
                _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
            }

            public IEnumerable<ITimesheetWeek> Weeks => _fixture.CreateMany<TestTimesheetWeek>();
        }

        public class TestTimesheetWeek : ITimesheetWeek
        {
            private readonly IFixture _fixture;

            public TestTimesheetWeek(IFixture fixture)
            {
                _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
            }

            public IWorkDay[] WorkDays => _fixture.Build<TestWorkDay>()
                                                  .Without(wd => wd.WorkedHours)
                                                  .CreateMany()
                                                  .Cast<IWorkDay>()
                                                  .ToArray();
        }

        public class TestWorkDay : IWorkDay
        {
            public DateTime Date { get; set; }

            public TimeSpan StartTime { get; set; }

            public TimeSpan EndTime { get; set; }

            public TimeSpan BreakDuration { get; set; }

            public TimeSpan WorkedHours { get; set; }

            public bool OnTimesheet { get; set; }
        }
    }
}