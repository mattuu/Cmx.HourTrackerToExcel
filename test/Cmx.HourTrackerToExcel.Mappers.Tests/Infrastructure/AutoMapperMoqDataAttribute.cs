using System.Diagnostics.CodeAnalysis;
using Cmx.HourTrackerToExcel.TestUtils;

namespace Cmx.HourTrackerToExcel.Mappers.Tests.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperMoqDataAttribute : AutoMoqDataAttribute
    {
        public AutoMapperMoqDataAttribute()
        {
            Fixture.Customize(new AutoMapperCustomization());
        }
    }
}
