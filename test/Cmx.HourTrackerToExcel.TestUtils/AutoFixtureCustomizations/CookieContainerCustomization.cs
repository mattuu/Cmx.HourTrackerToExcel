using Ploeh.AutoFixture;

namespace J2BI.Holidays.PCPS.TestUtils.AutoFixtureCustomizations
{
    public class CookieContainerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new CookieContainerSpecimenBuilder());
        }
    }
}