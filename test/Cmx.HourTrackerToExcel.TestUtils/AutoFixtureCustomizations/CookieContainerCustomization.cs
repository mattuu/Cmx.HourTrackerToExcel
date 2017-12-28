using AutoFixture;

namespace Cmx.HourTrackerToExcel.TestUtils.AutoFixtureCustomizations
{
    public class CookieContainerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new CookieContainerSpecimenBuilder());
        }
    }
}