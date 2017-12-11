using System.Diagnostics.CodeAnalysis;
using J2BI.Holidays.PCPS.Entities;

namespace J2BI.Holidays.PCPS.TestUtils.Attributes
{
    [ExcludeFromCodeCoverage]
    public class AutoEntitiesMoqDataAttribute : AutoMoqDataAttribute
    {
        public AutoEntitiesMoqDataAttribute()
        {
            Fixture.Customize<Area>(cc => cc.Empty());
            Fixture.Customize<AreaExtra>(cc => cc.Without(ae => ae.Area));
            Fixture.Customize<Distance>(cc => cc.Empty());
            Fixture.Customize<Facility>(cc => cc.Empty());
            Fixture.Customize<Parking>(cc => cc.Empty());
            Fixture.Customize<Pool>(cc => cc.Empty());
            Fixture.Customize<Property>(cc => cc.Empty());
            Fixture.Customize<PropertyVersion>(cc => cc.Empty());
            Fixture.Customize<Provider>(cc => cc.Without(p => p.Properties));
            Fixture.Customize<Transaction>(cc => cc.Without(t => t.PropertyVersion));
        }
    }
}