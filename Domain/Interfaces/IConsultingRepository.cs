using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IConsultingRepository
    {
        Task<Hotel> GetHotel(int IdHotel);
        Task<IEnumerable<Hotel>> GetHotels(int? IdHotel);
        Task<Usuario> GetUser(int UserId);
    }
}
