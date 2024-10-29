using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

       
        public int BookId { get; set; }
      
       
        public Book Book { get; set; } = new Book();
    
    }

}
