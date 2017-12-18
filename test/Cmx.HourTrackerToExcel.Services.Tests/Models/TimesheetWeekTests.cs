using System;
using System.Globalization;
using System.Linq;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.Services.Models;
using Cmx.HourTrackerToExcel.TestUtils;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Shouldly;
using Xunit;

namespace Cmx.HourTrackerToExcel.Services.Tests.Models
{
    public class TimesheetWeekTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(TimesheetWeek).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void Add_ShouldThrowExceptionWhenTryingToAddMoreThanSevenDaysToTheWeek(IFixture fixture, DateTime startDate, TimesheetWeek sut)
        {
            // arrange..
            for (var dt = startDate; dt < startDate.AddDays(7); dt = dt.AddDays(1))
            {
                var workDay = fixture.Build<WorkDay>()
                                     .With(wd => wd.Date, dt)
                                     .Create();
                sut.AddDay(workDay);
            }

            // act..
            var actual = Record.Exception(() => sut.AddDay(fixture.Create<WorkDay>()));

            // assert..
            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<ArgumentException>();
            actual.Message.ShouldBe("Attempt on adding more than 7 days to a week");
        }

        [Theory, AutoMoqData]
        public void Add_ShouldThrowExceptionWhenTryingToAddSameWeekDayTwice(IFixture fixture, DateTime startDate, TimesheetWeek sut)
        {
            // arrange..
            var workDay = fixture.Build<WorkDay>()
                                 .With(wd => wd.Date, startDate)
                                 .Create();
            
            sut.AddDay(workDay);

            // act..
            var actual = Record.Exception(() => sut.AddDay(fixture.Build<WorkDay>()
                                                                  .With(wd => wd.Date, startDate.AddDays(7))
                                                                  .Create()));

            // assert..
            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<ArgumentException>();
        }

        [Theory, AutoMoqData]
        public void WeekDays_ShouldReturnCorrectResult(IFixture fixture, TimesheetWeek sut)
        {
            // arrange..
            var startDate = new DateTime(2017, 12, 18); // Monday..
            for (var dt = startDate; dt < startDate.AddDays(7); dt = dt.AddDays(1))
            {
                var workDay = fixture.Build<WorkDay>()
                                     .With(wd => wd.Date, dt)
                                     .Create();
                sut.AddDay(workDay);
            }

            // act..
            var actual = sut.WorkDays;

            // assert..
            actual.ShouldContain(wd => wd.Date.DayOfWeek == DayOfWeek.Monday);
            actual.ShouldContain(wd => wd.Date.DayOfWeek == DayOfWeek.Tuesday);
            actual.ShouldContain(wd => wd.Date.DayOfWeek == DayOfWeek.Wednesday);
            actual.ShouldContain(wd => wd.Date.DayOfWeek == DayOfWeek.Thursday);
            actual.ShouldContain(wd => wd.Date.DayOfWeek == DayOfWeek.Friday);
            actual.ShouldContain(wd => wd.Date.DayOfWeek == DayOfWeek.Saturday);
            actual.ShouldContain(wd => wd.Date.DayOfWeek == DayOfWeek.Sunday);
        }

        [Theory, AutoMoqData]
        public void WeekDays_ShouldReturnDataInCorrectOrder(IFixture fixture, DateTime startDate, TimesheetWeek sut)
        {
            // arrange..
            for (var dt = startDate; dt < startDate.AddDays(7); dt = dt.AddDays(1))
            {
                var workDay = fixture.Build<WorkDay>()
                                     .With(wd => wd.Date, dt)
                                     .Create();
                sut.AddDay(workDay);
            }

            // act..
            var actual = sut.WorkDays;

            // assert..
            actual.ElementAt(0).Date.DayOfWeek.ShouldBe(DayOfWeek.Monday);
            actual.ElementAt(1).Date.DayOfWeek.ShouldBe(DayOfWeek.Tuesday);
            actual.ElementAt(2).Date.DayOfWeek.ShouldBe(DayOfWeek.Wednesday);
            actual.ElementAt(3).Date.DayOfWeek.ShouldBe(DayOfWeek.Thursday);
            actual.ElementAt(4).Date.DayOfWeek.ShouldBe(DayOfWeek.Friday);
            actual.ElementAt(5).Date.DayOfWeek.ShouldBe(DayOfWeek.Saturday);
            actual.ElementAt(6).Date.DayOfWeek.ShouldBe(DayOfWeek.Sunday);
        }

        [Theory, AutoMoqData]
        public void WeekDays_ShouldPadLeft(IFixture fixture, TimesheetWeek sut)
        {
            // arrange..
            var startDate = new DateTime(2017, 12, 20); // Wednesday..
            for (var dt = startDate; dt < startDate.AddDays(5); dt = dt.AddDays(1))
            {
                var workDay = fixture.Build<WorkDay>()
                                     .With(wd => wd.Date, dt)
                                     .Create();
                sut.AddDay(workDay);
            }

            // act..
            var actual = sut.WorkDays;

            // assert..
            actual.ElementAt(0).ShouldBeNull();
            actual.ElementAt(1).ShouldBeNull();
            actual.ElementAt(2).Date.DayOfWeek.ShouldBe(DayOfWeek.Wednesday);
            actual.ElementAt(3).Date.DayOfWeek.ShouldBe(DayOfWeek.Thursday);
            actual.ElementAt(4).Date.DayOfWeek.ShouldBe(DayOfWeek.Friday);
            actual.ElementAt(5).Date.DayOfWeek.ShouldBe(DayOfWeek.Saturday);
            actual.ElementAt(6).Date.DayOfWeek.ShouldBe(DayOfWeek.Sunday);
        }
    }
}