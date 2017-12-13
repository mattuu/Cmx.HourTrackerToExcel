using System.Diagnostics.CodeAnalysis;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Cmx.HourTrackerToExcel.TestUtils
{
    [ExcludeFromCodeCoverage]
    public class InlineAutoMapperMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMapperMoqDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values))
        {
        }
    }
}
