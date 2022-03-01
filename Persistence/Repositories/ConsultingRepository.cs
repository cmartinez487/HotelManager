using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ConsultingRepository: IConsultingRepository
    {
        #region Instancia
        private readonly string _conn;

        public ConsultingRepository(string conn)
        {
            _conn = conn;
        }

        #endregion

        public async Task<Hotel> GetHotel(int IdHotel)
        {
            DataSet ds = new DataSet();
            using (SqlConnection Conn = new SqlConnection(_conn))
            {
                using (SqlCommand cmd = new SqlCommand("spGetHotel", Conn))
                {
                    cmd.Parameters.AddWithValue("@IdHotel", IdHotel);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }
            }
            var dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                return await Task.FromResult<Hotel>(null);
            }


            var result = (from row in ds.Tables[0].AsEnumerable()
                          select new Hotel
                          {
                              IdHotel = row.Field<int>("IdHotel"),
                              Nombre = row.Field<string>("nombre"),
                              Pais = row.Field<string>("pais"),
                              Descripcion = row.Field<string>("descripcion"),
                              Capacidad = row.Field<int>("Capacidad"),
                              Latitud = row.Field<int>("latitud"),
                              Longitud = row.Field<int>("longitud"),
                              Status = row.Field<bool>("activo"),

                          }).FirstOrDefault();

            return await Task.FromResult<Hotel>(result);
        }

        public async Task<Usuario> GetUser(int UserId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection Conn = new SqlConnection(_conn))
            {
                using (SqlCommand cmd = new SqlCommand("spGetUser", Conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }
            }
            var dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                return await Task.FromResult<Usuario>(null);
            }
            var result = (from row in ds.Tables[0].AsEnumerable()
                          select new Usuario
                          {
                              UserId = row.Field<int>("UserId"),
                              Nombre = row.Field<string>("nombre"),
                              Apellidos = row.Field<string>("apellidos"),
                              Mail = row.Field<string>("mail"),
                              Direccion = row.Field<string>("direccion")
                          }).FirstOrDefault();

            return await Task.FromResult<Usuario>(result);
        }

        public async Task<IEnumerable<Hotel>> GetHotels(int? IdHotel)
        {
            DataSet ds = new DataSet();
            using (SqlConnection Conn = new SqlConnection(_conn))
            {
                using (SqlCommand cmd = new SqlCommand("spGetHotels", Conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }
            }
            var dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                return await Task.FromResult<IEnumerable<Hotel>>(null);
            }
            var result = (from row in ds.Tables[0].AsEnumerable()
                          select new Hotel
                          {
                              IdHotel = row.Field<int>("IdHotel"),
                              Nombre = row.Field<string>("nombre"),
                              Pais = row.Field<string>("pais"),
                              Descripcion = row.Field<string>("descripcion"),
                              Capacidad = row.Field<int>("Capacidad"),
                              Latitud = row.Field<int>("latitud"),
                              Longitud = row.Field<int>("longitud"),
                              Status = row.Field<bool>("activo"),

                          }).ToList();

            return await Task.FromResult<IEnumerable<Hotel>>(result);
        }
    }
 }
