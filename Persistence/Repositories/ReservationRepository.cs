using Dapper;
using Domain.Dtos.Parameters;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Clients;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        #region Instancia
        private readonly string _conn;
        private readonly SqlServerClient _cliService;

        public ReservationRepository(string conn, SqlServerClient cliService)
        {
            _conn = conn;
            _cliService = cliService;
        }

        #endregion

        /// <summary>
        /// Cancelacion de Reserva
        /// </summary>
        /// <param name="Reserva"></param>
        /// <returns></returns>
        public async Task CancelReservation(Reservacion Reserva)
        {
            try
            {
                using (SqlConnection Conn = new SqlConnection(_conn))
                {
                    using (SqlCommand cmd = new SqlCommand("spCancelReservation", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdReserva", Reserva.IdReserva);
                        cmd.Parameters.AddWithValue("@Idhotel", Reserva.IdHotel);
                        await Conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message},{e.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Creacion de Reserva
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task CreateReservation(ReserParam param)
        {
            try
            {
                using (SqlConnection Conn = new SqlConnection(_conn))
                {
                    using (SqlCommand cmd = new SqlCommand("spCreateReservation", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", param.UserId);
                        cmd.Parameters.AddWithValue("@Idhotel", param.IdHotel);
                        cmd.Parameters.AddWithValue("@checkin", param.StartDate.Date);
                        cmd.Parameters.AddWithValue("@checkout", param.EndDate.Date);
                        cmd.Parameters.AddWithValue("@hab", 1);
                        await Conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message},{e.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// devuelve un listado de reservas por id de usuario, id de hotel un rango de fechas segun su creacion
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Reservacion>> GetReservationListUser(ReserParam param)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", param.UserId);
            parameters.Add("@IdHotel", param.IdHotel);
            parameters.Add("@StartDate", param.StartDate);
            parameters.Add("@EndDate", param.EndDate);
            var sql = $@"
                         select IdReserva, R.UserId UserId, R.Idhotel Idhotel, checkin, checkout, 
                                fechaReserva, Estado status, IdHab, U.mail UserMail, H.nombre HotelNombre
                         from reserva R
                         join Usuario U on r.UserId = U.UserId
                         join Hotel H on r.Idhotel = H.Idhotel
                         where R.UserId = @UserId and R.Idhotel = @IdHotel
                         and fechaReserva between @StartDate and @EndDate";
        
            return await _cliService.RetrieveMultipleRowsAsync<Reservacion>(sql, parameters, null);
        }

        /// <summary>
        /// devuelve un listado de reservas activas
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Reservacion>> GetReservationActive()
        {
            var sql = $@"
                         select IdReserva, R.UserId UserId, R.Idhotel Idhotel, checkin, checkout, 
                                fechaReserva, Estado status, IdHab, U.mail UserMail, H.nombre HotelNombre
                         from reserva R
                         join Usuario U on r.UserId = U.UserId
                         join Hotel H on r.Idhotel = H.Idhotel
                         where Estado = 1";

            return await _cliService.RetrieveMultipleRowsAsync<Reservacion>(sql, null,null);
        }

        /// <summary>
        /// devielve la reserva por id de reserva
        /// </summary>
        /// <param name="IdReserva"></param>
        /// <returns></returns>
        public async Task<Reservacion> GetReservation(int IdReserva)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IdReserva", IdReserva);
            var sql = $@"select IdReserva, R.UserId UserId, R.Idhotel Idhotel, checkin, checkout,
                                fechaReserva, Estado status, IdHab, U.mail UserMail, H.nombre HotelNombre
                         from reserva R
                         join Usuario U on r.UserId = U.UserId
                         join Hotel H on r.Idhotel = H.Idhotel
                         where IdReserva = @IdReserva";

            return await _cliService.RetrieveSingleRowAsync<Reservacion>(sql, parameters, null);
        }

        /// <summary>
        /// devuelve la reserva segun el usuario y comparando el checkin y el checkout
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Reservacion> GetReservationUser(ReserParam param)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", param.UserId);
            parameters.Add("@IdHotel", param.IdHotel);
            parameters.Add("@checkin", param.StartDate.Date);
            parameters.Add("@checkout", param.EndDate.Date);
            var sql = $@"select IdReserva, R.UserId UserId, R.Idhotel Idhotel, checkin, checkout,
                                fechaReserva, Estado status, IdHab, U.mail UserMail, H.nombre HotelNombre
                         from reserva R
                         join Usuario U on r.UserId = U.UserId
                         join Hotel H on r.Idhotel = H.Idhotel
                         where R.UserId = @UserId and R.Idhotel = @IdHotel
                         and checkin = @checkin and checkout = @checkout";

            return await _cliService.RetrieveSingleRowAsync<Reservacion>(sql, parameters, null);
        }
    }
}
