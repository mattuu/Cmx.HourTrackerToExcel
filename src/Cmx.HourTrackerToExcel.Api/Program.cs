using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cmx.HourTrackerToExcel.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .UseContentRoot(Directory.GetCurrentDirectory())
                       .ConfigureLogging((hostingContext, logging) =>
                                         {
                                             logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                                             logging.AddConsole();
                                             logging.AddDebug();
                                         })
                       .ConfigureWebHostDefaults(webBuilder =>
                                                 {
                                                     webBuilder.UseKestrel()
                                                               .UseIISIntegration()
                                                               .UseIIS()
                                                               .UseStartup<Startup>();
                                                 });
        }
    }
}