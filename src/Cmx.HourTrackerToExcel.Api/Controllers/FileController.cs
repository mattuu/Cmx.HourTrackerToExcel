using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cmx.HourTrackerToExcel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Cmx.HourTrackerToExcel.Api.Controllers
{
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly ICsvToTimesheetConverter _csvToTimesheetConverter;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        public FileController(ICsvToTimesheetConverter csvToTimesheetConverter, IFileProvider fileProvider, ILogger<FileController> logger)
        {
            _csvToTimesheetConverter = csvToTimesheetConverter ??
                                       throw new ArgumentNullException(nameof(csvToTimesheetConverter));
            _fileProvider = fileProvider ?? throw new ArgumentException(nameof(fileProvider));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFileCollection formFiles)
        {
            var accessToken = Request.Headers["X-AccessToken"];
            if (string.IsNullOrEmpty(accessToken))
            {
                return BadRequest("X-AccessToken header is missing");
            }

            // full path to file in temp location
            var formFile = formFiles.First();

            _logger.LogInformation($"Received file {formFile.FileName}");

            if (formFile.Length > 0)
            {
                if (!CreateTimeslip(accessToken))
                {
                    return BadRequest("Unable to submit timeslip to FA");
                }

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

        private bool CreateTimeslip(string accessToken)
        {
            var client = new RestClient("https://api.sandbox.freeagent.com/v2/");

            var request = new RestRequest("timeslips", Method.POST);
            request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            request.AddHeader("Authorization", $"Bearer {accessToken}");

            request.AddJsonBody(new TimeslipWrapperModel()
            {
                Timeslip = new TimeslipCreateModel()
                {
                    Task = "https://api.sandbox.freeagent.com/v2/tasks/4551",
                    Project = "https://api.sandbox.freeagent.com/v2/projects/5691",
                    User = "https://api.sandbox.freeagent.com/v2/users/2467",
                    DatedOn = DateTime.Today,
                    Hours = 8.2M
                }
            });

            var response = client.Execute(request);

            return response.IsSuccessful;
        }
    }

    public class TimeslipWrapperModel
    {
        [JsonProperty("timeslip")]
        public TimeslipCreateModel Timeslip { get; set; }
    }

    public class TimeslipCreateModel
    {
        [JsonProperty("task")]
        public string Task { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("dated_on")]
        public DateTime DatedOn { get; set; }

        [JsonProperty("hours")]
        public decimal Hours { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }

    public class NewtonsoftJsonSerializer : ISerializer, IDeserializer
    {
        private Newtonsoft.Json.JsonSerializer serializer;

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string ContentType
        {
            get { return "application/json"; } // Probably used for Serialization?
            set { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static NewtonsoftJsonSerializer Default
        {
            get
            {
                return new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
        }
    }
}