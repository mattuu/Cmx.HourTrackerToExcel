using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;

namespace Cmx.HourTrackerToExcel.Api.Controllers
{
    [Route("api/file")]
    public class FileController : Controller
    {
        [HttpGet]
        public Task<IActionResult> Get()
        {
            var filePath = $@"C:\temp\upload";
            var fileName = "PATH.txt";

            //using (var readStream = new FileStream(filePath, FileMode.Open))
            //{
            //    var memory = new MemoryStream();
            //    await readStream.CopyToAsync(memory);

            return Task.Run(() =>
            {

                IFileProvider provider = new PhysicalFileProvider(filePath);
                IFileInfo fileInfo = provider.GetFileInfo(fileName);
                var readStream = fileInfo.CreateReadStream();
                var mimeType = "text/plain";

                Response.Headers.Add("Content-Disposition", "attachment");

                return (IActionResult) File(readStream, mimeType, fileName);
            });
            //}
        }

        [HttpPost]
        public async Task<IActionResult> Post(Model model)
        {
            // full path to file in temp location
            var file = model.File;
            var fileName = model.File.FileName;

            var filePath = $@"C:\temp\upload\{file.FileName}";
            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            IFileProvider provider = new PhysicalFileProvider(@"C:\temp\upload");
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();

            return File(readStream, file.ContentType, fileName);
        }
    }

    public class Model
    {
        public IFormFile File { get; set; }
    }
}