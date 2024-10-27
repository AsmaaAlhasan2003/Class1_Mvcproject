using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public ActivityType ActivityType { get; set; }
        public DateTime Date { get; set; }
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }
    }

}
