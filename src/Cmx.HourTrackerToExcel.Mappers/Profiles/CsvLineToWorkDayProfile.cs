using AutoMapper;
using Cmx.HourTrackerToExcel.Models.Export;
using Cmx.HourTrackerToExcel.Models.Import;

namespace Cmx.HourTrackerToExcel.Mappers.Profiles
{
    public class CsvLineToWorkDayProfile : Profile 
    {
        public CsvLineToWorkDayProfile()
        {
            CreateMap<CsvLine, WorkDay>()
                .ForMember(wd => wd.Date, cfg => cfg.MapFrom(csv => csv.ClockedIn.Date))
                .ForMember(wd => wd.StartTime, cfg => cfg.MapFrom(csv => csv.ClockedIn.TimeOfDay))
                .ForMember(wd => wd.EndTime, cfg => cfg.MapFrom(csv => csv.ClockedOut.TimeOfDay))
                .ForMember(wd => wd.BreakDuration, cfg => cfg.MapFrom(csv => -csv.TotalTimeAdjustment));
        }
    }
}
