using Domain.Entities;
using Domain.Dtos.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservacion>> GetReservationListUser(ReserParam param);
        Task<Reservacion> GetReservation(int IdReserva);
        Task CreateReservation(ReserParam param);
        Task CancelReservation(Reservacion Reserva);
        Task<Reservacion> GetReservationUser(ReserParam param);
        Task<IEnumerable<Reservacion>> GetReservationActive();
    }
}
