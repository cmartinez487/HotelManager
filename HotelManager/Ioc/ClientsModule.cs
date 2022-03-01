using Autofac;
using Microsoft.Extensions.Configuration;
using Persistence.Clients;

namespace HotelManager.Ioc
{
    public class ClientsModule: Module
    {
        private readonly IConfiguration _configuration;
        public ClientsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((context, parameters) =>
            {
                string connection = _configuration.GetSection("ConnectionStrings:DBConnection").Value;
                return new SqlServerClient(connection);
            })
            .As<SqlServerClient>()
            .SingleInstance();

            base.Load(builder);
        }
    }
}
