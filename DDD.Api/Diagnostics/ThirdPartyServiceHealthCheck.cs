using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.Api.Diagnostics
{
    public class ThirdPartyServiceHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                return Task.FromResult(HealthCheckResult.Healthy("ThirdParty services running ok.."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("ThirdParty services NOT running.."));
        }
    }
}
