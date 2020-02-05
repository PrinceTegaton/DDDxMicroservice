using DDD.Api.Authentication;
using DDD.Core.Managers;
using DDD.Infrastructure.DataAccess;
using DDD.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rext;

namespace DDD.Api
{
    public partial class Startup
    {
        // register app DI
        // this keeps the Startup.cs class neat

        public IServiceCollection ConfigureDI(IServiceCollection services)
        {
            services.AddMvc(opt =>
                    {
                        // enable all api endpoint requests to be authenticated
                        // to override specific action or controllers, add AllowAnonymous to it
                        // as seen on SystemController.cs/Ping()
                        opt.Filters.Add(new AuthorizeFilter());
                    })
                    .AddJsonOptions(opt =>
                    {
                        opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IProfileManager, ProfileManager>()
                    .AddTransient<IRextHttpClient, RextHttpClient>();

            services.AddTransient<IAuthorizationHandler, APIKeyStoreHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // bind SimpleRepository
            services.AddTransient<DbContext, AppDbContext>();
            services.AddScoped(typeof(ISimpleRepository<>), typeof(SimpleRepository<>));

            // simple HttpClient util
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
