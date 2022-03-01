using Application.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultingController : ControllerBase
    {
        #region Instance

        private readonly ConsultingService _consultService;

        public ConsultingController(ConsultingService consultService)
        {
            _consultService = consultService;
        }

        #endregion

        /// <summary>
        /// Consulta usuario segun su id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("/get-User")]
        [Produces("application/json")]
        public async Task<ActionResult<Usuario>> GetUser([FromQuery] int userId)
        {
            return Ok(await _consultService.GetUser(userId));
        }

        /// <summary>
        /// Consulta Hotel segun su Id
        /// </summary>
        /// <param name="IdHotel"></param>
        /// <returns></returns>
        [HttpGet("/get-Hotel")]
        [Produces("application/json")]
        public async Task<ActionResult<Hotel>> GetHotel([FromQuery] int IdHotel)
        {
            return Ok(await _consultService.GetHotel(IdHotel));
        }
    }
}
