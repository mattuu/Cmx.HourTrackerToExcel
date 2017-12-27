using System;
using System.Collections.Generic;
using System.Linq;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.TestUtils;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Shouldly;
using Xunit;

namespace Cmx.HourTrackerToExcel.Services.Tests
{
    public class TimesheetInitializerTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(TimesheetInitializer).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void Initialize_ShouldAddAllWeekDaysToTimesheetWeeks(IFixture fixture, TimesheetInitializer sut)
        {
            // arrange..
            var startDate = new DateTime(2017, 12, 19);
            const int daysOffset = 10;

            var workDays = new HashSet<IWorkDay>();
            for (var dt = startDate; dt < startDate.AddDays(daysOffset); dt = dt.AddDays(1))
            {
                workDays.Add(fixture.Build<WorkDay>()
                                    .With(wd => wd.Date, dt)
                                    .Create());
            }

            // act..
            var actual = sut.Initialize(workDays);

            // assert..
            actual.Weeks.Count().ShouldBe(2);
            actual.Weeks.ElementAt(0).WorkDays.Length.ShouldBe(7);

            actual.Weeks.ElementAt(0).WorkDays.ElementAt(0).Date.ShouldBe(new DateTime(2017, 12, 18), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(0).WorkDays.ElementAt(1).Date.ShouldBe(new DateTime(2017, 12, 19), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(0).WorkDays.ElementAt(2).Date.ShouldBe(new DateTime(2017, 12, 20), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(0).WorkDays.ElementAt(3).Date.ShouldBe(new DateTime(2017, 12, 21), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(0).WorkDays.ElementAt(4).Date.ShouldBe(new DateTime(2017, 12, 22), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(0).WorkDays.ElementAt(5).Date.ShouldBe(new DateTime(2017, 12, 23), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(0).WorkDays.ElementAt(6).Date.ShouldBe(new DateTime(2017, 12, 24), TimeSpan.FromSeconds(3));

            actual.Weeks.ElementAt(1).WorkDays.Length.ShouldBe(7);
            actual.Weeks.ElementAt(1).WorkDays.ElementAt(0).Date.ShouldBe(new DateTime(2017, 12, 25), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(1).WorkDays.ElementAt(1).Date.ShouldBe(new DateTime(2017, 12, 26), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(1).WorkDays.ElementAt(2).Date.ShouldBe(new DateTime(2017, 12, 27), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(1).WorkDays.ElementAt(3).Date.ShouldBe(new DateTime(2017, 12, 28), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(1).WorkDays.ElementAt(4).Date.ShouldBe(new DateTime(2017, 12, 29), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(1).WorkDays.ElementAt(5).Date.ShouldBe(new DateTime(2017, 12, 30), TimeSpan.FromSeconds(3));
            actual.Weeks.ElementAt(1).WorkDays.ElementAt(6).Date.ShouldBe(new DateTime(2017, 12, 31), TimeSpan.FromSeconds(3));
        }

        [Theory, AutoMoqData]
        public void Initialize_ShouldHandleFirstDayOfWeekProperly(IFixture fixture, DateTime startDate, byte dayCount, TimesheetInitializer sut)
        {
            // arrange..
            startDate = startDate.Date;
            var workDays = new HashSet<IWorkDay>();
            for (var dt = startDate; dt < startDate.AddDays(dayCount); dt = dt.AddDays(1))
            {
                workDays.Add(fixture.Build<WorkDay>()
                                    .With(wd => wd.Date, dt)
                                    .Create());
            }

            // act..
            var actual = sut.Initialize(workDays);

            // assert..
            actual.Weeks.ShouldAllBe(tw => tw.WorkDays.First().Date.DayOfWeek == DayOfWeek.Monday);
        }

        [Theory, AutoMoqData]
        public void Initialize_ShouldFillAllWeeks(IFixture fixture, DateTime startDate, byte dayCount, TimesheetInitializer sut)
        {
            // arrange..
            startDate = startDate.Date;
            var workDays = new HashSet<IWorkDay>();
            for (var dt = startDate; dt < startDate.AddDays(dayCount); dt = dt.AddDays(1))
            {
                workDays.Add(fixture.Build<WorkDay>()
                                    .With(wd => wd.Date, dt)
                                    .Create());
            }

            // act..
            var actual = sut.Initialize(workDays);

            // assert..
            actual.Weeks.ShouldAllBe(tw => tw.WorkDays.Length == 7);
        }
    }
}