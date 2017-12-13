using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

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
