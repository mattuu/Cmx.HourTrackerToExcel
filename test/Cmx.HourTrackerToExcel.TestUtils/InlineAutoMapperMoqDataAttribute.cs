using System;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Cmx.HourTrackerToExcel.TestUtils
{
    [ExcludeFromCodeCoverage]
    public class InlineAutoMapperMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMapperMoqDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values))
        {
        }
    }

    [ExcludeFromCodeCoverage]
    public class AutoMapperCustomization : ICustomization
    {
        private readonly Func<IMapper> configuredMapperFunc;

        public AutoMapperCustomization(Func<IMapper> configuredMapperFunc)
        {
            this.configuredMapperFunc = configuredMapperFunc;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Customize<IMapper>(cc => cc.FromFactory(() => this.configuredMapperFunc()));
            //fixture.Customize<IMapper>(cc => cc.FromFactory(() => AutoMapperConfiguration.GetConfiguredMapper(t => fixture.Create(t, new SpecimenContext(fixture)))));
        }
    }

    [ExcludeFromCodeCoverage]
    public class AutoMapperMoqDataAttribute : AutoMoqDataAttribute
    {
        public AutoMapperMoqDataAttribute()
        {
            //Fixture.Customize(new AutoMapperCustomization());
        }
    }
}
