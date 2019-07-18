using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using AutoFixture.Idioms;
using Xunit;
using AutoFixture;
using System;
using Moq;
using AutoFixture.Xunit2;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Models.Export;

namespace Cmx.HourTrackerToExcel.Export.Tests
{
    public class TimesheetWeekExporterTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert
            assertion.Verify(typeof(TimesheetWeekExporterTests).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void Export_ShouldWriteEntireTimesheetWeek([Frozen] Mock<ITimesheetExportManager> timesheetExportManagerMock,
            IFixture fixture,
            TimesheetWeekExporter sut)
        {
            // arrange
            var workDays = fixture.Build<WorkDay>()
                .With(m => m.Date, DateTime.Parse("2019-07-15"))
                .CreateMany(1);

            var timesheetWeek = fixture.Build<ITimesheetWeek>()
                // .With(m => m.WorkDays, workDays)
                .Create();

            // act
            sut.Export(timesheetExportManagerMock.Object, timesheetWeek);
        }
    }
}
