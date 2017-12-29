using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cmx.HourTrackerToExcel.Api.Controllers
{
    [Route("api")]
    public class FileController : Controller
    {
        [HttpPost("file")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new
            {
                file.Length,
                filePath
            });
        }
    }
}