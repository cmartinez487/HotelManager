using Autofac;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Persistence.Clients;
using Persistence.Repositories;

namespace HotelManager.Ioc
{
    public class RepositoriesModule : Module
    {
        private readonly IConfiguration _configuration;
        public RepositoriesModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register((context, parameters) =>
                {
                    string conn = _configuration.GetConnectionString("DBConnection");
                    return new ConsultingRepository(conn);
                })
                .As<IConsultingRepository>()
                .SingleInstance();

                builder.Register((context, parameters) =>
                {
                    string conn = _configuration.GetConnectionString("DBConnection");
                    SqlServerClient client = context.Resolve<SqlServerClient>();

                    return new ReservationRepository(conn, client);
                })
                .As<IReservationRepository>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
