using Ploeh.AutoFixture.Idioms;
using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using Xunit;

namespace Cmx.HourTrackerToExcel.Export.Tests
{
    public class TimesheetFactoryTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(TimesheetFactory).GetConstructors());
        }
    }
}
