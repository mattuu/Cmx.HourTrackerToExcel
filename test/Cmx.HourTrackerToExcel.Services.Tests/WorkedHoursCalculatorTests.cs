﻿using System;
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
            workDay.StartTime.Minutes.ShouldBe(adjustedMins, $"Failing condition: original: {originalMins}, adjusted: {adjustedMins}");
        }

        [Theory]
        [InlineAutoMoqData(20, 20)]
        [InlineAutoMoqData(24, 20)]
        [InlineAutoMoqData(25, 30)]
        [InlineAutoMoqData(28, 30)]
        [InlineAutoMoqData(30, 30)]
        public void AdjustTimes_ShouldSetEndTime(int originalMins, int adjustedMins, IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, 30, 0))
                                 .With(wd => wd.BreakDuration, new TimeSpan(1, 0, 0))
                                 .With(wd => wd.WorkedHours, new TimeSpan(8, 0, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(17, originalMins, 0))
                                 .Create();


            // act..
            sut.AdjustTimes(workDay);

            // assert..
            workDay.EndTime.Minutes.ShouldBe(adjustedMins, $"Failing condition: original: {originalMins}, adjusted: {adjustedMins}");
        }

        [Theory]
        [InlineAutoMoqData(20, 20)]
        [InlineAutoMoqData(23, 30)]
        [InlineAutoMoqData(28, 30)]
        [InlineAutoMoqData(30, 30)]
        public void AdjustTimes_ShouldSetBreakDuration(int originalMins, int adjustedMins, IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.BreakDuration, new TimeSpan(0, originalMins, 0))
                                 .Create();

            // act..
            sut.AdjustTimes(workDay);

            // assert..
            workDay.BreakDuration.Minutes.ShouldBe(adjustedMins, $"Failing condition: original: {originalMins}, adjusted: {adjustedMins}");
        }

        [Theory]
        [InlineAutoMoqData(8, 30, 17, 30, 1, 0, 8, 0)]
        [InlineAutoMoqData(8, 42, 14, 1, 0, 0, 5, 20)]
        [InlineAutoMoqData(8, 46, 17, 31, 0, 32, 8, 20)]
        [InlineAutoMoqData(8, 25, 16, 35, 0, 0, 8, 10)]
        public void VerifyTimes_ShouldNotThrowException_WhenTimeDifferenceMatchesWorkHours(int startTimeHour,
                                                                                           int startTimeMinute,
                                                                                           int endTimeHour,
                                                                                           int endTimeMinute,
                                                                                           int breakDurationHour,
                                                                                           int breakDurationMinute,
                                                                                           int workedHours,
                                                                                           int workedMinutes,
                                                                                           IFixture fixture,
                                                                                           WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(startTimeHour, startTimeMinute, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(endTimeHour, endTimeMinute, 0))
                                 .With(wd => wd.BreakDuration, new TimeSpan(breakDurationHour, breakDurationMinute, 0))
                                 .With(wd => wd.WorkedHours, new TimeSpan(workedHours, workedMinutes, 0))
                                 .Create();

            // act..
            var actual = Record.Exception(() => sut.VerifyTimes(workDay));

            // assert..
            actual.ShouldBeNull($"Failing condition: {startTimeHour}:{startTimeMinute}-{endTimeHour}:{endTimeMinute}, break: {breakDurationHour}:{breakDurationMinute}, worked hours: {workedHours}:{workedMinutes}");
        }

        [Theory, AutoMoqData]
        public void VerifyTimes_ShouldThrowException_WhenTimeDifferenceNotMatchWorkHours(IFixture fixture, WorkedHoursCalculator sut)
        {
            // arrange..
            var workDay = fixture.Build<TestWorkDay>()
                                 .With(wd => wd.StartTime, new TimeSpan(8, 30, 0))
                                 .With(wd => wd.EndTime, new TimeSpan(17, 30, 0))
                                 .With(wd => wd.BreakDuration, new TimeSpan(1, 0, 0))
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

            public bool OnTimesheet { get; set; }
        }
    }
}