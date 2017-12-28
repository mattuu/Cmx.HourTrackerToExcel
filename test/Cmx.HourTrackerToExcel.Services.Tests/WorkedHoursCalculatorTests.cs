using System;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using AutoFixture;
using AutoFixture.Idioms;
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

        [Theory, AutoMoqData]
        public void Calculate_ShouldReturnCorrectResult_WhenBreakDurationIsZero(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, 30, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(16, 30, 0))
                                 .With(wd => wd.BreakDuration, TimeSpan.Zero)
                                 .Create();

            // act..
            var actual = sut.Calculate(workDay);

            // assert..
            actual.ShouldBe(new TimeSpan(8, 0, 0));
        }

        [Theory, AutoMoqData]
        public void Calculate_ShouldReturnCorrectResult_WhenBreakDurationIsNotZero(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, 0, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(17, 30, 0))
                                 .With(wd => wd.BreakDuration, new TimeSpan(1, 0, 0))
                                 .Create();

            // act..
            var actual = sut.Calculate(workDay);

            // assert..
            actual.ShouldBe(new TimeSpan(8, 30, 0));
        }

        [Theory, AutoMoqData]
        public void Calculate_ShouldReturnCorrectResult_WhenStartTimeHasMinutes(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, 34, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(17, 00, 0))
                                 .With(wd => wd.BreakDuration, TimeSpan.Zero)
                                 .Create();

            // act..
            var actual = sut.Calculate(workDay);

            // assert..
            actual.ShouldBe(new TimeSpan(8, 30, 0));
        }

        [Theory, AutoMoqData]
        public void Calculate_ShouldReturnCorrectResult_WhenEndTimeHasMinutes(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, 30, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(17, 08, 0))
                                 .With(wd => wd.BreakDuration, TimeSpan.Zero)
                                 .Create();

            // act..
            var actual = sut.Calculate(workDay);

            // assert..
            actual.ShouldBe(new TimeSpan(8, 40, 0));
        }

        [Theory, AutoMoqData]
        public void Calculate_ShouldReturnCorrectResult_WhenBreakDurationHasMinutes(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, 30, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(17, 30, 0))
                                 .With(wd => wd.BreakDuration, new TimeSpan(0, 24, 0))
                                 .Create();

            // act..
            var actual = sut.Calculate(workDay);

            // assert..
            actual.ShouldBe(new TimeSpan(8, 40, 0));
        }


        [Theory, AutoMoqData]
        public void Calculate_ShouldThrowArgumentException_WhenStartTimeIsNull(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .Without(wd => wd.StartTime)
                                 .Create();

            // act..
            var actual = Record.Exception(() => sut.Calculate(workDay));

            // assert..
            actual.ShouldBeOfType<ArgumentException>();
            actual.Message.ShouldContain("StartTime");
        }

        [Theory, AutoMoqData]
        public void Calculate_ShouldThrowArgumentException_WhenEndTimeIsNull(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .Without(wd => wd.EndTime)
                                 .Create();

            // act..
            var actual = Record.Exception(() => sut.Calculate(workDay));

            // assert..
            actual.ShouldBeOfType<ArgumentException>();
            actual.Message.ShouldContain("EndTime");
        }

        private class TestWorkDay : IWorkDay
        {
            public DateTime Date { get; }

            public TimeSpan StartTime { get; set; }

            public TimeSpan EndTime { get; set; }

            public TimeSpan BreakDuration { get; set; }

            public TimeSpan? WorkedHours { get; }
        }
    }
}