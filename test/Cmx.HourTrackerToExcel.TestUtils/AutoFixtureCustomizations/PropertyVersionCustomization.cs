using J2BI.Holidays.PCPS.Entities;
using Ploeh.AutoFixture;

namespace J2BI.Holidays.PCPS.TestUtils.AutoFixtureCustomizations
{
    public class PropertyVersionCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<PropertyVersion>(p => p.OmitAutoProperties());
            fixture.Customize<Parking>(p => p.OmitAutoProperties());
        }
    }
}