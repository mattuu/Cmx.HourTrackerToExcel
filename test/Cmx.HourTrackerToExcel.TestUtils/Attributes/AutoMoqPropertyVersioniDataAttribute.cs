using J2BI.Holidays.PCPS.TestUtils.AutoFixtureCustomizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

namespace J2BI.Holidays.PCPS.TestUtils.Attributes
{
    public class AutoMoqPropertyVersioniDataAttribute : AutoDataAttribute
    {
        public AutoMoqPropertyVersioniDataAttribute()
            : base(new Fixture().Customize(new PropertyVersionCustomization()))
        {
        }
    }
}