using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Exhibition 
    {
        public int Exhibitionid { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Book> books { get; set; } = new List<Book>();
        public List<Visitor> visitors { get; set; } = new List<Visitor>();  

      /*  public Exhibition(int id, string name, DateTime startDate, DateTime endDate)
           
        {
            Exhibitionid = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            books = new List<Book>();
            visitors = new List<Visitor>();
        }*/
    }

}
