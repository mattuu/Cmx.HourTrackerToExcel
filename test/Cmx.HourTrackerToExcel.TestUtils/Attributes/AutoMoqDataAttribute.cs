using System.Diagnostics.CodeAnalysis;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace Cmx.HourTrackerToExcel.TestUtils
{
    [ExcludeFromCodeCoverage]
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(new Fixture())
        {
            Fixture.Customize(new AutoMoqCustomization())
                   .Behaviors.Add(new OmitOnRecursionBehavior());

        }
    }
}
