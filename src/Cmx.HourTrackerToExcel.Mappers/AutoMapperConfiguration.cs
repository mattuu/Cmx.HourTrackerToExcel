using System;
using AutoMapper;
using Cmx.HourTrackerToExcel.Mappers.Profiles;

namespace Cmx.HourTrackerToExcel.Mappers
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetConfiguredMapper(Func<Type, object> resolverFunc)
        {
            var config = new MapperConfiguration(cfg =>
            {
                Configure(cfg);
            });

            config.AssertConfigurationIsValid();

            return new Mapper(config);
        }

        public static void Configure(IMapperConfigurationExpression cfg, Func<Type, object> resolverFunc = null)
        {
            cfg.AddProfile<CsvLineToWorkDayProfile>();

            cfg.AllowNullCollections = true;

            cfg.Advanced.AllowAdditiveTypeMapCreation = true;

            if (resolverFunc != null)
            {
                cfg.ConstructServicesUsing(resolverFunc);
            }
        }
    }
}
