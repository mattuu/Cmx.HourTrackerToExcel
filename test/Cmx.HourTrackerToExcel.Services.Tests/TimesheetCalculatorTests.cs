using System;
using System.Linq;
using AutoFixture.Xunit2;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using Moq;
using AutoFixture.Idioms;
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
        public void Calculate_ShouldPopulateDailyWorkHours(ITimesheet timesheet, [Frozen] Mock<IWorkedHoursCalculator> workedHoursCalculatorMock, TimesheetCalculator sut)
        {
            // arrange..
            //workedHoursCalculatorMock.Setup(m => m.Calculate(It.IsAny<IWorkDay>())).Returns(TimeSpan.Zero);

            // act..
            sut.CalculateWorkingHours(timesheet);

            // assert..
            timesheet.Weeks.SelectMany(tw => tw.WorkDays).ShouldAllBe(wd => wd.WorkedHours == TimeSpan.Zero);
        }
    }
}