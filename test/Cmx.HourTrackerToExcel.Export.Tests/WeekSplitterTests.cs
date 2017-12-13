using Ploeh.AutoFixture.Idioms;
using Cmx.HourTrackerToExcel.TestUtils;
using Xunit;
using Ploeh.AutoFixture;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using System.Collections.Generic;
using Cmx.HourTrackerToExcel.Models.Export;
using System;
using System.Linq;
using Shouldly;

namespace Cmx.HourTrackerToExcel.Export.Tests
{
    public class WeekSplitterTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(WeekSplitter).GetConstructors());
        }

        //[Theory, AutoMoqData]
        public void Split_ShouldReturnCorrectResult(IFixture fixture, WeekSplitter sut)
        {
            // arrange..
            var workDays = new HashSet<IWorkDay>();

            for (var i = 1; i < 30; i++)
            {
                var workDay = fixture.Build<WorkDay>()
                                     .With(wd => wd.Date, new DateTime(i, 12, 2017))
                                     .Create();
                workDays.Add(workDay);
            }

            // act..
            var actual = sut.Split(workDays, DayOfWeek.Monday);

            // assert..
            actual.Count().ShouldBe(5);
        }
    }
}
