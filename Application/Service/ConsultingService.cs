using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ConsultingService
    {
        #region Instance
        private readonly IConsultingRepository _consultRepository;

        public ConsultingService(IConsultingRepository consultRepository)
        {
            _consultRepository = consultRepository;
        }
        #endregion
        public async Task<Hotel> GetHotel(int IdHotel)
        {
            return await _consultRepository.GetHotel(IdHotel);
        }
        public async Task<Usuario> GetUser(int UserId)
        {
            return await _consultRepository.GetUser(UserId);
        }
    }
}
