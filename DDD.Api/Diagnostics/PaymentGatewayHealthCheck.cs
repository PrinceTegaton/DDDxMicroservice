using Microsoft.Extensions.Diagnostics.HealthChecks;
using Rext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.Api.Diagnostics
{
    public class PaymentGatewayHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var rext = new RextHttpClient();
            var rsp = rext.GetString("http://google.com").GetAwaiter().GetResult();

            if (rsp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Payment Gateway API is running.."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Payment Gateway API is NOT running.."));
        }
    }
}
