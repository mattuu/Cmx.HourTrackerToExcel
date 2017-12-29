using AutoMapper;
using Cmx.HourTrackerToExcel.Export;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Mappers;
using Cmx.HourTrackerToExcel.Services;
using Unity;
using Unity.Injection;

namespace Cmx.HourTrackerToExcel.App
{
    public static class UnityConfig
    {
        public static IUnityContainer GetConfiguredContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IWorkedHoursCalculator, WorkedHoursCalculator>();
            container.RegisterType<ITimesheetInitializer, TimesheetInitializer>();
            container.RegisterType<ITimesheetValidator, TimesheetValidator>();

            container.RegisterType<ICsvDataReader, CsvDataReader>();

            container.RegisterType<IMapper>(new InjectionFactory(c => AutoMapperConfiguration.GetConfiguredMapper(t => c.Resolve(t))));

            container.RegisterType<ITimesheetWeekExporter, TimesheetWeekExporter>();
            container.RegisterType<ITimesheetExportManager, TimesheetExportManager>();

            return container;
        }
    }
}