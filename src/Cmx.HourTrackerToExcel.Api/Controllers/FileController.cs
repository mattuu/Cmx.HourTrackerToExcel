using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Cmx.HourTrackerToExcel.Api.Controllers
{
    [Route("api/file")]
    public class FileController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post(Model model)
        {
            // full path to file in temp location
            var file = model.File;

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

    public class Model {
        public IFormFile File { get; set; }
    }
}