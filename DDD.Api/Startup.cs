using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Api.Diagnostics;
using DDD.Infrastructure.DataAccess;
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

namespace DDD.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string conn = Configuration.GetConnectionString("Roadmapp.Profile");
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(conn);
            });

            services.AddHealthChecks()
                    .AddDbContextCheck<AppDbContext>("SQL Server Database")
                    .AddCheck<ThirdPartyServiceHealthCheck>("ThirdParty Services")
                    .AddCheck<PaymentGatewayHealthCheck>("Payment Gateway");
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
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
