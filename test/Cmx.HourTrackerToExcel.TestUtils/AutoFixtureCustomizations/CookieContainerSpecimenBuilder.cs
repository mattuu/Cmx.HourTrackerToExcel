using System.Net;
using System.Reflection;
using Ploeh.AutoFixture.Kernel;

namespace J2BI.Holidays.PCPS.TestUtils.AutoFixtureCustomizations
{
    public class CookieContainerSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as PropertyInfo;
            if (pi != null && pi.PropertyType == typeof(CookieContainer))
            {
                return new CookieContainer(1, 1, 10000);
            }

            return new NoSpecimen();
        }
    }
}