using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Api.Diagnostics;
using DDD.Core.Managers;
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
using Rext;

namespace DDD.Api
{
    public partial class Startup
    {
        // register app DI
        // this keeps the Startup.cs class neat

        public IServiceCollection ConfigureDI(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IProfileManager, ProfileManager>()
                    .AddTransient<IRextHttpClient, RextHttpClient>();

            services.AddTransient<DbContext, AppDbContext>();
            services.AddScoped(typeof(ISimpleRepository<>), typeof(SimpleRepository<>));

            SetupRext();

            return services;
        }

        void SetupRext()
        {
            RextHttpClient.Setup(opt =>
            {
                opt.HttpConfiguration = new RextHttpCongifuration
                {
                    ProxyAddress = null, // NetworkProxy if you are behind a corporate firewall,
                    RelaxSslCertValidation = true 
                };
            });
        }
    }
}
