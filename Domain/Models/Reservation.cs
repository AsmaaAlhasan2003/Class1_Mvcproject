using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string EventType { get; set; }
        public DateTime ReservationDate { get; set; }
        public ReservationType ReservationType { get; set; }
        public int VisitorId { get; set; }
       
        public Visitor Visitor { get; set; }
    }

}
