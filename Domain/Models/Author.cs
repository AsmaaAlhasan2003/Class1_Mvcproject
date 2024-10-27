using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Author 
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Country { get; set; }
        public DateTime BirthDay { get; set; }
        public List<Book> Books  {get; set; }

      /*  public Author(int id, string name, Gender gender, string country, DateTime birthDay)
            
        {
            AuthorId = id;
            Name= name;
            Gender = gender;
            Country = country;
            BirthDay = birthDay;
            Books = new List<Book>();
        }*/
    }


}
