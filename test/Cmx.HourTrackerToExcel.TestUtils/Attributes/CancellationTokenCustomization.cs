using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Ploeh.AutoFixture;

namespace J2BI.Holidays.PCPS.TestUtils.Attributes
{
    [ExcludeFromCodeCoverage]
    public class CancellationTokenCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() => new CancellationTokenSource().Token);
        }
    }
}