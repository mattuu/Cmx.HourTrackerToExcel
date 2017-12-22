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
        public void Initialize_ShouldAddAllWeekDaysToTimesheetWeeks(IFixture fixture, DateTime startDate, int daysOffset, DayOfWeek firstDayOfWeek, TimesheetInitializer sut)
        {
            // arrange..
            startDate = new DateTime(2017, 12, 19);
            firstDayOfWeek = DayOfWeek.Monday;
            daysOffset = 10;

            var workDays = new HashSet<IWorkDay>();
            for (var dt = startDate; dt < startDate.AddDays(daysOffset); dt = dt.AddDays(1))
            {
                workDays.Add(fixture.Build<WorkDay>()
                                .With(wd => wd.Date, dt)
                                .Create());
            }

            // act..
            var actual = sut.Initialize(workDays, firstDayOfWeek);

            // assert..
            actual.ElementAt(0).WorkDays.ElementAt(0).Date.ShouldBe(new DateTime(2017, 12, 18), TimeSpan.FromSeconds(3));
            actual.ElementAt(0).WorkDays.ElementAt(1).Date.ShouldBe(new DateTime(2017, 12, 19), TimeSpan.FromSeconds(3));
            //actual.ElementAt(0).WorkDays.ElementAt(2).Date.ShouldBe(new DateTime(2017, 12, 20), TimeSpan.FromSeconds(3));
            //actual.ElementAt(0).WorkDays.ElementAt(3).Date.ShouldBe(new DateTime(2017, 12, 21), TimeSpan.FromSeconds(3));
            //actual.ElementAt(0).WorkDays.ElementAt(4).Date.ShouldBe(new DateTime(2017, 12, 22), TimeSpan.FromSeconds(3));
            //actual.ElementAt(0).WorkDays.ElementAt(5).Date.ShouldBe(new DateTime(2017, 12, 23), TimeSpan.FromSeconds(3));
            //actual.ElementAt(0).WorkDays.ElementAt(6).Date.ShouldBe(new DateTime(2017, 12, 24), TimeSpan.FromSeconds(3));
            //actual.ElementAt(1).WorkDays.ElementAt(0).Date.ShouldBe(new DateTime(2017, 12, 25), TimeSpan.FromSeconds(3));
        }
    }
}