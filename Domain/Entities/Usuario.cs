using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Usuario
    {
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Mail { get; set; }
        public string Direccion { get; set; }

    }
}
