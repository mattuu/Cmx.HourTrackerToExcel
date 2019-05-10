using System.IO;
using AutoMapper;
using Cmx.HourTrackerToExcel.Export;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Mappers;
using Cmx.HourTrackerToExcel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Cmx.HourTrackerToExcel.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.GetTempPath()));

            services.AddTransient<IFormFile, FormFile>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Cmx.HourTrackerToExcel.Api",
                        Version = "v1"
                    });
            });

            services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder =>
                        {
                            builder.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials()
                                .WithExposedHeaders("X-FileName", "X-AccessToken");
                        });
                })
                .AddMvcCore();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            services.AddMvc();

            services.AddAutoMapper(cfg => { AutoMapperConfiguration.Configure(cfg); });

            services.AddTransient<ICsvToTimesheetConverter, CsvToTimesheetConverter>()
                .AddTransient<ICsvDataReader, CsvDataReader>()
                .AddTransient<ITimesheetInitializer, TimesheetInitializer>()
                .AddTransient<ITimesheetValidator, TimesheetValidator>()
                .AddTransient<ITimesheetExportManager, TimesheetExportManager>()
                .AddTransient<IWorkedHoursCalculator, WorkedHoursCalculator>()
                .AddTransient<ITimesheetWeekExporter, TimesheetWeekExporter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cmx.HourTrackerToExcel.Api V1"); });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}