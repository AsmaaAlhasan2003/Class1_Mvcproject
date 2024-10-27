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
        public string Type { get; set; }
        public DateTime DateOfPublication { get; set; }
        public double Price { get; set; }
        public BookSection Section { get; set; }
        public BookStatus Status { get; set; }

        // علاقة مع المعرض
       public int? ExhibitionId { get; set; }
        public Exhibition? Exhibition { get; set; } = new Exhibition();

        // قائمة الاستعارات للكتاب
         public List<Loan>? Loans { get; set; } = new List<Loan>();
        [NotMapped]
        public string FilePath { get; set; }
        
    }
}
