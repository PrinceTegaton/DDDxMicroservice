using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Api.Diagnostics;
using DDD.Api.Extensions;
using DDD.Infrastructure.DataAccess;
using DDD.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace DDD.Api
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDI(services);

            services.AddCors(); // disable to enforce strict security on app
                       

            // connect to SQL Server database
            string conn = Configuration.GetConnectionString("DDD.Conn");
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(conn);
                opt.EnableSensitiveDataLogging(); // turn off as required to keep logs cleaner
            });

            services.AddLogging();

            // register HealthCheck services
            services.AddHealthChecks()
                    .AddDbContextCheck<AppDbContext>("SQL Server Database")
                    .AddCheck<ThirdPartyServiceHealthCheck>("ThirdParty Services")
                    .AddCheck<PaymentGatewayHealthCheck>("Payment Gateway");

            // setup Swagger
            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DDD.Api",
                    Version = "v1",
                    Description = "Power-packed Microservice template with Domain-Driven-Design pattern"
                });
                a.ResolveConflictingActions(b => b.First());

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                a.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // set health response
            var healthOptions = new HealthCheckOptions();
            healthOptions.ResultStatusCodes[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable;
            healthOptions.ResponseWriter = CustomHealthCheckResponseWriter.WriteResponse;
            app.UseHealthChecks("/health", healthOptions);

            // condigure global try...catch
            app.UseMiddleware<ExceptionMiddleware>();

            // configure swagger
            app.UseSwagger()
               .UseSwaggerUI(a =>
            {
                a.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD.Api");
                //a.RoutePrefix = Configuration["AppSettings:VirtualDirectory"];
            });

            // disable to enforce strict security on app
            app.UseCors(opt => opt
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
