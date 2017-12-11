using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AutoMapper;
using Ploeh.AutoFixture.Kernel;

namespace J2BI.Holidays.PCPS.TestUtils.AutoFixtureCustomizations
{
    [ExcludeFromCodeCoverage]
    public class MapperCustomization : ISpecimenBuilder
    {
        private readonly IMapper _mapper;

        public MapperCustomization(IMapper mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as ParameterInfo;
            if (pi == null || pi.ParameterType != typeof(IMapper))
            {
                return new NoSpecimen();
            }

            return _mapper;
        }
    }
}