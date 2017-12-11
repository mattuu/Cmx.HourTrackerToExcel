using Ploeh.AutoFixture.Xunit2;
using Xunit;
using Xunit.Sdk;

namespace J2BI.Holidays.PCPS.TestUtils.Attributes
{
    public class InlineWebApiAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineWebApiAutoMoqDataAttribute(params object[] values)
            : base(new DataAttribute[]
            {
                new InlineDataAttribute(values), new WebApiAutoMoqDataAttribute()
            })
        {
        }
    }
}