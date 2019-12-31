using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Cmx.HourTrackerToExcel.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .UseKestrel()
                      .UseIISIntegration()
                      .UseStartup<Startup>()
                      .ConfigureLogging((hostingContext, logging) =>
                                        {
                                            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                                            logging.AddConsole();
                                            logging.AddDebug();
                                        })
                      .Build();

            host.Run();
        }
    }
}