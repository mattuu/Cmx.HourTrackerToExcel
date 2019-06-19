using AutoMapper;
using Cmx.HourTrackerToExcel.Common.Interfaces;
using Cmx.HourTrackerToExcel.Models.Export;

namespace Cmx.HourTrackerToExcel.Mappers.Profiles
{
    public class CsvLineToWorkDayProfile : Profile
    {
        public CsvLineToWorkDayProfile()
        {
            CreateMap<ICsvLine, WorkDay>()
                .ForMember(wd => wd.Date, cfg => cfg.MapFrom(csv => csv.ClockedIn.Date))
                .ForMember(wd => wd.StartTime, cfg => cfg.MapFrom(csv => csv.ClockedIn.TimeOfDay))
                .ForMember(wd => wd.EndTime, cfg => cfg.MapFrom(csv => csv.ClockedOut.TimeOfDay))
                .ForMember(wd => wd.BreakDuration, cfg => cfg.MapFrom(csv => -csv.TotalTimeAdjustment))
                .ForMember(wd => wd.WorkedHours, cfg => cfg.MapFrom(csv => csv.Duration))
                .ForMember(wd => wd.OnTimesheet, cfg => cfg.Ignore());
        }
    }
}
