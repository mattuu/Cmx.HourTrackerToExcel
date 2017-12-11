using System.Diagnostics.CodeAnalysis;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace J2BI.Holidays.PCPS.TestUtils.Attributes
{
    [ExcludeFromCodeCoverage]
    public class InlineAutoEntitiesMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoEntitiesMoqDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values), new AutoEntitiesMoqDataAttribute())
        {
        }
    }
}