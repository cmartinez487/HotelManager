using Domain.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Reservacion: DateRange
    {
        public int IdReserva { get; set; }
        public int UserId { get; set; }
        public string UserMail { get; set; }
        public int IdHotel { get; set; }
        public string HotelNombre { get; set; }
        public int IdHab { get; set; }
        public DateTime FechaReserva { get; set; }
        public bool status { get; set; }
    }
}
