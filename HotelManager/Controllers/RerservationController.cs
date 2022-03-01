using Application.Service;
using Domain.Entities;
using Domain.Dtos.Parameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RerservationController : ControllerBase
    {
        #region Instance

        private readonly ReservationService _reservaService;

        public RerservationController(ReservationService reservaService)
        {
            _reservaService = reservaService;
        }

        #endregion

        /// <summary>
        /// Endpoint que crea la lista de reservaciones activas
        /// </summary>
        /// <returns></returns>
        [HttpGet("/get-reservation-active")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Reservacion>>> GetReservation()
        {
            return Ok(await _reservaService.GetReservationActive());
        }
        /// <summary>
        /// Endpoint que Genera la lista de reservaciones por Usuario y un rango de fecha
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Idhotel"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet("/get-reservation-for-user")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Reservacion>>> GetReservationListUser([FromQuery] int userId, int Idhotel, DateTime start, DateTime end)
        {
            var consult = new ReserParam()
            {
                UserId = userId,
                IdHotel = Idhotel,
                StartDate = start,
                EndDate = end
            };

            return Ok(await _reservaService.GetReservationListUser(consult));
        }

        /// <summary>
        /// Endpoint que crear las reservaciones
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("/create-reservation")]
        [Produces("application/json")]
        public async Task<ActionResult<Reservacion>> CreateReservation([FromBody] ReserParam param)
        {
            return Ok(await _reservaService.CreateReservation(param));
        }

        /// <summary>
        /// Endpoint que cancela las reservaciones
        /// </summary>
        /// <param name="IdReserva"></param>
        /// <returns></returns>
        [HttpPost("/cancel-reservation")]
        [Produces("application/json")]
        public async Task<ActionResult<Reservacion>> CancelReservation([FromQuery] int IdReserva)
        {
            return Ok(await _reservaService.CancelReservation(IdReserva));
        }
    }
}
