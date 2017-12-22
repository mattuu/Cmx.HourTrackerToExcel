using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.TestUtils;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Shouldly;
using Xunit;

namespace Cmx.HourTrackerToExcel.Services.Tests
{
    public class TimesheetCalculatorTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(TimesheetCalculator).GetConstructors());
        }

        [Theory, AutoMoqData]
        public async Task Aggregate_ShouldAddAllWeekDaysToTimesheetWeeks(IFixture fixture, DateTime startDate, int daysOffset, TimesheetCalculator sut)
        {
            // arrange..
            var days = new HashSet<IWorkDay>();
            for (var dt = startDate; dt < startDate.AddDays(daysOffset); dt = dt.AddDays(1))
            {
                days.Add(fixture.Build<WorkDay>()
                                .With(wd => wd.Date, dt)
                                .Create());
            }

            // act..
            var actual = await sut.Calculate(days);

            // assert..
            foreach (var day in days)
            {
                actual.SelectMany(t => t.WorkDays).ShouldContain(day, $"Failed on {day.Date:d}");
            }
        }
    }
}