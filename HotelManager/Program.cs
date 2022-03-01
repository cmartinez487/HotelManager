using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var pathToContentRoot = AppDomain.CurrentDomain.BaseDirectory;
            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(pathToContentRoot)
                .AddJsonFile("appsettings.json", true)
                .Build();
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices(services => { services.AddAutofac(); })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddConfiguration(configurationRoot);
                    config.SetBasePath(pathToContentRoot);
                    config.AddJsonFile("appsettings.Development.json", true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                });

        }
    }
}
