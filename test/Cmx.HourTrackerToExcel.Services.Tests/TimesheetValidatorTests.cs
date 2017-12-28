using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using Moq;
using Shouldly;
using Xunit;

namespace Cmx.HourTrackerToExcel.Services.Tests
{
    public class TimesheetValidatorTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(TimesheetValidator).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void Validate_ShouldCall_AdjustTimes_On_IWorkedHoursCalculator_ForEveryWorkDay(TestTimesheet timesheet, [Frozen] Mock<IWorkedHoursCalculator> workedHoursCalculatorMock, TimesheetValidator sut)
        {
            // act..
            sut.AdjustTimesheet(timesheet);

            // assert..
            timesheet.Weeks.ShouldNotBeEmpty();
            timesheet.Weeks.SelectMany(tw => tw.WorkDays).ShouldNotBeEmpty();

            var days = timesheet.Weeks.SelectMany(tw => tw.WorkDays);
            workedHoursCalculatorMock.Verify(m => m.AdjustTimes(It.IsAny<IWorkDay>()), Times.Exactly(days.Count()));
        }

        [Theory, AutoMoqData]
        public void Validate_ShouldCall_VerifyTimes_On_IWorkedHoursCalculator_ForEveryWorkDay(TestTimesheet timesheet, [Frozen] Mock<IWorkedHoursCalculator> workedHoursCalculatorMock, TimesheetValidator sut)
        {
            // act..
            sut.AdjustTimesheet(timesheet);

            // assert..
            timesheet.Weeks.ShouldNotBeEmpty();
            timesheet.Weeks.SelectMany(tw => tw.WorkDays).ShouldNotBeEmpty();

            var days = timesheet.Weeks.SelectMany(tw => tw.WorkDays);
            workedHoursCalculatorMock.Verify(m => m.VerifyTimes(It.IsAny<IWorkDay>()), Times.Exactly(days.Count()));
        }

        [Theory, AutoMoqData]
        public void Validate_ShouldThrowException_WhenTimesheetContainsInvalid(TestTimesheet timesheet, [Frozen] Mock<IWorkedHoursCalculator> workedHoursCalculatorMock, TimesheetValidator sut)
        {
            // act..
            sut.AdjustTimesheet(timesheet);

            // assert..
            timesheet.Weeks.ShouldNotBeEmpty();
            timesheet.Weeks.SelectMany(tw => tw.WorkDays).ShouldNotBeEmpty();

            var days = timesheet.Weeks.SelectMany(tw => tw.WorkDays);
            workedHoursCalculatorMock.Verify(m => m.VerifyTimes(It.IsAny<IWorkDay>()), Times.Exactly(days.Count()));
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