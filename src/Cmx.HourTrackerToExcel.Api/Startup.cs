using System.IO;
using AutoMapper;
using Cmx.HourTrackerToExcel.Export;
using Cmx.HourTrackerToExcel.Import;
using Cmx.HourTrackerToExcel.Mappers;
using Cmx.HourTrackerToExcel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
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

            services.AddSwaggerGen(c =>
                                   {
                                       c.SwaggerDoc("v1",
                                                    new OpenApiInfo
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
                                                              .WithExposedHeaders("X-FileName");
                                                   });
                             });

            services.AddControllers()
                    .AddNewtonsoftJson();

            
            services.AddLogging(loggingBuilder =>
                                {
                                    loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                                    loggingBuilder.AddConsole();
                                    loggingBuilder.AddDebug();
                                });
            
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSwagger()
               .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cmx.HourTrackerToExcel.Api V1"); });

            app.UseRouting();

            app.UseEndpoints(builder =>
                             {
                                 //builder.MapControllers(); 
                                 builder.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                             });

        }
    }
}