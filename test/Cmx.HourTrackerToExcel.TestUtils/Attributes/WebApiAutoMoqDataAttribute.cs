using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

namespace J2BI.Holidays.PCPS.TestUtils.Attributes
{
    public class WebApiAutoMoqDataAttribute : AutoDataAttribute
    {
        public WebApiAutoMoqDataAttribute()
            : base(new Fixture()
                       .Customize(new WebApiAutoMoqCustomization()))
        {
        }
    }
}