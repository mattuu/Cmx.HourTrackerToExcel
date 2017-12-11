using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace J2BI.Holidays.PCPS.TestUtils.AutoFixtureCustomizations
{
    public class AutoMoqUriCustomization : CompositeCustomization
    {
        public AutoMoqUriCustomization()
            : base(new AutoConfiguredMoqCustomization(),
                   new UriCustomization(),
                   new CookieContainerCustomization())
        {
        }
    }
}