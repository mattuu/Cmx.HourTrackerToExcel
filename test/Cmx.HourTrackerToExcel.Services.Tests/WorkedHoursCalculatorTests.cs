using System;
using AutoFixture;
using AutoFixture.Idioms;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using Shouldly;
using Xunit;

namespace Cmx.HourTrackerToExcel.Services.Tests
{
    public class WorkedHoursCalculatorTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(WorkedHoursCalculator).GetConstructors());
        }

        [Theory]
        [InlineAutoMoqData(20, 20)]
        [InlineAutoMoqData(23, 20)]
        [InlineAutoMoqData(28, 20)]
        [InlineAutoMoqData(30, 30)]
        public void AdjustTimes_ShouldSetStartTime(int originalMins, int adjustedMins, IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, originalMins, 0))
                                 .Create();

            // act..
            sut.AdjustTimes(workDay);

            // assert..
            workDay.StartTime.Minutes.ShouldBe(adjustedMins);
        }

        [Theory]
        [InlineAutoMoqData(20, 20)]
        [InlineAutoMoqData(23, 30)]
        [InlineAutoMoqData(28, 30)]
        [InlineAutoMoqData(30, 30)]
        public void AdjustTimes_ShouldSetEndTime(int originalMins, int adjustedMins, IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.EndTime, new TimeSpan(17, originalMins, 0))
                                 .Create();

            // act..
            sut.AdjustTimes(workDay);

            // assert..
            workDay.EndTime.Minutes.ShouldBe(adjustedMins);
        }

        [Theory, AutoMoqData]
        public void VerifyTimes_ShouldNotThrowException_WhenTimeDifferenceMatchesWorkHours(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, 34, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(17, 25, 0))
                                 .With(wd => wd.BreakDuration, new TimeSpan(1, 5, 0))
                                 .With(wd => wd.WorkedHours, new TimeSpan(8, 0, 0))
                                 .Create();

            // act..
            var actual = Record.Exception(() => sut.VerifyTimes(workDay));

            // assert..
            actual.ShouldBeNull();
        }

        [Theory, AutoMoqData]
        public void VerifyTimes_ShouldReturnFalse_WhenTimeDifferenceNotMatchWorkHours(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, 34, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(17, 28, 0))
                                 .With(wd => wd.BreakDuration, new TimeSpan(0, 42, 0))
                                 .With(wd => wd.WorkedHours, new TimeSpan(8, 30, 0))
                                 .Create();

            // act..
            var actual = Record.Exception(() => sut.VerifyTimes(workDay));

            // assert..
            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<ApplicationException>();
        }

        private class TestWorkDay : IWorkDay
        {
            public DateTime Date { get; set; }

            public TimeSpan StartTime { get; set; }

            public TimeSpan EndTime { get; set; }

            public TimeSpan BreakDuration { get; set; }

            public TimeSpan WorkedHours { get; set; }
        }
    }
}