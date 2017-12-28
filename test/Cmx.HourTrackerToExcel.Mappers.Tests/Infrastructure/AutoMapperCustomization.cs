using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;

namespace Cmx.HourTrackerToExcel.Mappers.Tests.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<IMapper>(cc => cc.FromFactory(() => AutoMapperConfiguration.GetConfiguredMapper(t => fixture.Create(t, new SpecimenContext(fixture)))));
        }
    }
}
