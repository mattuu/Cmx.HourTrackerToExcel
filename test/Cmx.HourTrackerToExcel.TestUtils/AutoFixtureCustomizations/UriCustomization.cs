using System;
using Ploeh.AutoFixture;

namespace J2BI.Holidays.PCPS.TestUtils.AutoFixtureCustomizations
{
    public class UriCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Uri>(cc => cc.FromFactory(() => new Uri("http://jet2.com")));
        }
    }
}