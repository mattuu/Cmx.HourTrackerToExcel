using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
}