using Domain.Dtos.Commons;
using System;

namespace Domain.Dtos.Parameters
{
    public class ReserParam
    {
        public int UserId { get; set; }
        public int IdHotel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
