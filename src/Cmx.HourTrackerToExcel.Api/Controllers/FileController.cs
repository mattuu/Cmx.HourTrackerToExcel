using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cmx.HourTrackerToExcel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Cmx.HourTrackerToExcel.Api.Controllers
{
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly ICsvToTimesheetConverter _csvToTimesheetConverter;
        private readonly IFileProvider _fileProvider;

        public FileController(ICsvToTimesheetConverter csvToTimesheetConverter, IFileProvider fileProvider)
        {
            _csvToTimesheetConverter = csvToTimesheetConverter ??
                                       throw new ArgumentNullException(nameof(csvToTimesheetConverter));
            _fileProvider = fileProvider ?? throw new ArgumentException(nameof(fileProvider));
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFileCollection formFiles)
        {
            // full path to file in temp location
            var formFile = formFiles.First();

            if (formFile.Length > 0)
            {
                var filePath = await _csvToTimesheetConverter.Convert(formFile);

                var destinationFileName = Path.GetFileName(filePath);

                var fileInfo = _fileProvider.GetFileInfo(destinationFileName);
                var readStream = fileInfo.CreateReadStream();
                var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.Headers.Add("Content-Disposition", "attachment");
                Response.Headers.Add("X-FileName", destinationFileName);

                return File(readStream, mimeType, destinationFileName);
            }

            ModelState.AddModelError("formFiles", "Uploading multiple files is not supported");

            return BadRequest(ModelState);
        }
    }
}