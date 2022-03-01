using Domain.Entities;
using Domain.Dtos.Parameters;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Application.Service
{
    
    public class ReservationService
    {
        #region Instance
        private readonly IReservationRepository _resevRepository;
        private readonly IConsultingRepository _consultRepository;

        public ReservationService(IReservationRepository resevRepository, IConsultingRepository consultRepository)
        {
            _resevRepository = resevRepository;
            _consultRepository = consultRepository;
        }
        #endregion

        public async Task<IEnumerable<Reservacion>> GetReservationActive()
        {
            return await _resevRepository.GetReservationActive();
        }

        public async Task<IEnumerable<Reservacion>> GetReservationListUser(ReserParam param)
        {
            var user = await _consultRepository.GetUser(param.UserId);
            var hotel = await _consultRepository.GetHotel(param.IdHotel);
            ValidateData(param, user, hotel);

            return await _resevRepository.GetReservationListUser(param);
        }

        public async Task<string> CreateReservation(ReserParam param)
        {
            var user = await _consultRepository.GetUser(param.UserId);
            var hotel = await _consultRepository.GetHotel(param.IdHotel);
            var reserva = await _resevRepository.GetReservationUser(param);
            ValidateData(param, user, hotel);
            ValidateReservaDate(param, reserva);

            await _resevRepository.CreateReservation(param);

            return "Reserva Creada";
        }

        public async Task<string> CancelReservation(int IdReserva)
        {
            var reserva = await _resevRepository.GetReservation(IdReserva);
            ValidateRerserva(reserva);

            await _resevRepository.CancelReservation(reserva);
            return "Reserva Cancelada";
        }

        /// <summary>
        /// Validaciones de existencias de usuario, hotel, capacidad del hotel y si las fechas estan de manera correcta
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user"></param>
        /// <param name="hotel"></param>
        private void ValidateData(ReserParam param, Usuario user, Hotel hotel)
        {
            if (param.StartDate == param.EndDate)
            {
                Log.Error("Las Fechas no pueden ser iguales");
                throw new Exception("Error, Las Fechas no pueden ser iguales");
            }

            if (param.StartDate > param.EndDate)
            {
                Log.Error("Las Fechas no pueden ser iguales");
                throw new Exception("Error, Verificar las fechas");
            }

            if (user == null || hotel == null)
            {
                Log.Error("Valide si existe el usuario o el hotel de consulta");
                throw new Exception("Error, Valide si existe el usuario o el hotel de consulta");
            }

            if (hotel.Capacidad == 0)
            {
                Log.Error("Error, no hay vacantes....");
                throw new Exception("Error, no hay vacantes....");

            }
        }

        /// <summary>
        /// Valida si existe una reserva en un rango de fecha, solo usada en la creaccion de reservas
        /// </summary>
        /// <param name="param"></param>
        /// <param name="reserva"></param>
        private void ValidateReservaDate(ReserParam param, Reservacion reserva)
        {
            if (reserva != null && reserva.status == true && reserva.Checkin == param.StartDate.Date && reserva.Checkout == param.EndDate.Date)
            {
                Log.Error("Error, ya existe una Reserva para esta fecha");
                throw new Exception("Error, ya existe una Reserva para esta fecha");
            }
        }

        /// <summary>
        /// Valida si existe una reserva y si su status es false
        /// </summary>
        /// <param name="reserva"></param>
        private void ValidateRerserva(Reservacion reserva)
        {
            if (reserva == null)
            {
                Log.Error("Error, La reserva no existe");
                throw new Exception("Error, La reserva no existe");
            }

            if (reserva.status == false)
            {
                Log.Error("Error, La reserva no esta activa");
                throw new Exception("Error, La reserva no esta activa");
            }
        }
    }
}
