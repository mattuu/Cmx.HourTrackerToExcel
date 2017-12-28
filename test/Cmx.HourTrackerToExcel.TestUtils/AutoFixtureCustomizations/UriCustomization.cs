using System;
using AutoFixture;

namespace Cmx.HourTrackerToExcel.TestUtils.AutoFixtureCustomizations
{
    public class UriCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Uri>(cc => cc.FromFactory(() => new Uri("http://jet2.com")));
        }
    }
}