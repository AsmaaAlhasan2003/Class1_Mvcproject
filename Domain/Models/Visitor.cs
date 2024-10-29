using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Visitor
    {
        public int? VisitorId { get; set; }
        public string Name { get; set; }
        public string ContactDetails { get; set; }
        public DateTime VisitDate { get; set; }
        public List<Activity> Reservations { get; set; } = new List<Activity>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();

    
        public List<Activity> activities { get; set; } = new List<Activity>();
    }

}