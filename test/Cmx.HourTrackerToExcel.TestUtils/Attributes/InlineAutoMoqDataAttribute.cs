using System.Diagnostics.CodeAnalysis;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace J2BI.Holidays.PCPS.TestUtils.Attributes
{
    [ExcludeFromCodeCoverage]
    public class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values), new AutoMoqDataAttribute())
        {
        }
    }
}