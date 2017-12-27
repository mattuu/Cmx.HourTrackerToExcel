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
        public void Calculate_ShouldPopulateDailyWorkHours(IFixture fixture, TimesheetCalculator sut)
        {
            //// arrange..
            //var timesheet = fixture.Build<ITimesheet>()
            //    .With(t => t.Weeks, fixture.Build<ITimesheetWeek>()
            //        .With(tw => tw.WorkDays, fixture.Build<IWorkDay>()
            //            .With(wd => wd.Date, fixture.Create<DateTime>())
            //            .With(wd => wd.StartTime, TimeSpan.Parse("08:34"))
            //            .With(wd => wd.EndTime, TimeSpan.Parse("08:34"))
            //            )
            //            ))


            //// act..
            //sut.Calculate(days);

            //// assert..
            //foreach (var day in days)
            //{
            //    actual.SelectMany(t => t.WorkDays).ShouldContain(day, $"Failed on {day.Date:d}");
            //}
        }
    }
}