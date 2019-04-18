using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Cmx.HourTrackerToExcel.Services
{
    public interface ICsvToTimesheetConverter
    {
        Task<string> Convert(IFormFile formFile);
    }
}