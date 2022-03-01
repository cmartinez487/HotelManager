using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Hotel
    {
        public int IdHotel { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }
        public int Latitud { get; set; }
        public int Longitud { get; set; }
        public bool Status { get; set; }
    }
}
