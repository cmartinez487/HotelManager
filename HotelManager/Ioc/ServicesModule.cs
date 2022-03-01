using Application.Service;
using Autofac;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HotelManager.Ioc
{
    public class ServicesModule : Module
    {
        private readonly IConfiguration _configuration;
        public ServicesModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((context, parameters) =>
            {
                IConsultingRepository consultRepository = context.Resolve<IConsultingRepository>();

                return new ConsultingService(consultRepository);
            })
            .As<ConsultingService>()
            .SingleInstance();

            builder.Register((context, parameters) =>
            {
                IConsultingRepository consultRepository = context.Resolve<IConsultingRepository>();
                IReservationRepository reservRepository = context.Resolve<IReservationRepository>();

                return new ReservationService(reservRepository, consultRepository);
            })
            .As<ReservationService>()
            .SingleInstance();

            base.Load(builder);
        }
    }
}
