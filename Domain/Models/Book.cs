using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } 
       public int? AuthorId {  get; set; }
        public Author? Author { get; set; }
       
        public DateTime DateOfPublication { get; set; }
        public double Price { get; set; }
        public BookSection Section { get; set; }
        public BookStatus Status { get; set; }



        // قائمة الاستعارات للكتاب
         public List<Loan>? Loans { get; set; } = new List<Loan>();
        
        public string? FilePath { get; set; }
        
    }
}
