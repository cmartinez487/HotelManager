using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManager.Ioc
{
    public class Modules:Module
    {
        private readonly IConfiguration Configuration;

        public Modules(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new ClientsModule(Configuration));
            builder.RegisterModule(new RepositoriesModule(Configuration));
            builder.RegisterModule(new ServicesModule(Configuration));
        }
    }
}
