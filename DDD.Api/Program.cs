using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DDD.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                        config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false);

                        // load appclients from file. remove this line if you want to use another means on auth
                        config.AddJsonFile("appclients.json", optional: false, reloadOnChange: true);
                    })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();

                //.ConfigureLogging(logging =>
                // {
                //     logging.ClearProviders();
                //     logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                // })
                //.UseNLog(); 
                
        // enable to use NLog as default Log engine. 
        // NLog binds seamlessly to dotnet-core log interface
    }
}
