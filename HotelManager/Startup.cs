using Autofac;
using HotelManager.Ioc;
using HotelManager.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace HotelManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            AddSwagger(services);
            ConfigureSerilog();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelManager v1");
                });
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<HandlerExceptionMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new Modules(Configuration));
        }

        private void ConfigureSerilog()
        {
            //var cloudAwatchConfig = new AWSLoggerConfig();
            //Configuration.GetSection("CloudWatch:Serilog").Bind(cloudAwatchConfig);
            var formatterLog = new Serilog.Formatting.Json.JsonFormatter();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Console()
                .CreateLogger();
            Log.ForContext("Fecha", DateTime.Now.ToString())
                .Warning("Inicia el servicio");
        }


        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"HotelManager {groupName}",
                    Version = groupName,
                    Description = "HotelManager"
                });
            });
        }
    }
}
