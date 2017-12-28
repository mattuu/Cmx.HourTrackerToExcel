using AutoFixture;
using AutoFixture.AutoMoq;

namespace Cmx.HourTrackerToExcel.TestUtils.AutoFixtureCustomizations
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