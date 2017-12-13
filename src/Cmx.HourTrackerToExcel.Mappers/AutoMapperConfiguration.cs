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
                cfg.AddProfile<CsvLineToWorkDayProfile>();

                cfg.AllowNullCollections = true;

                cfg.Advanced.AllowAdditiveTypeMapCreation = true;

                cfg.ConstructServicesUsing(resolverFunc);
            });

            config.AssertConfigurationIsValid();

            return new Mapper(config);
        }
    }
}
