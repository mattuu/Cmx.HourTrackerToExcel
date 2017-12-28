using System.Diagnostics.CodeAnalysis;
using AutoFixture.Xunit2;
using Xunit;

namespace Cmx.HourTrackerToExcel.TestUtils.Attributes
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
