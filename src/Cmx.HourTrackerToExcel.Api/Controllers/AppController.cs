using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("[controller]")]
public class AppController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        string version = fvi.FileVersion;
        return Ok(version);
    }

    [HttpPost]
    public IActionResult RegisterOAuthToken([FromBody] AccessTokenModel token)
    {
        Debugger.Break();

        return Ok();
    }
}

public class AccessTokenModel
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

}