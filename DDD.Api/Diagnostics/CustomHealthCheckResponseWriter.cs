using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Api.Diagnostics
{
    public class CustomHealthCheckResponseWriter
    {
        public static Task WriteResponse(HttpContext context, HealthReport report)
        {
            string result = JsonConvert.SerializeObject(new
            {
                status = report.Status.ToString(),
                available_services = report.Entries.Count(a => a.Value.Status == HealthStatus.Healthy),
                unavailable_services = report.Entries.Count(a => a.Value.Status == HealthStatus.Unhealthy),
                services = report.Entries.Select(a => new
                {
                    component = a.Key,
                    status = a.Value.Status.ToString(),
                    status_code = a.Value.Status
                })
            });

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
