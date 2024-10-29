using ApplicationData.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationData.Repositories
{
    public class LoanRepository : IRepository<Loan>
    {
        private readonly MnagementBdContext _context;

        public LoanRepository(MnagementBdContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .ToListAsync();
        }

        public async Task<Loan> GetByIdAsync(int loanId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.LoanId == loanId);
        }

        public async Task<Loan> AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await SaveChangesAsync();
            return loan;
        }

        public async Task UpdateAsync(Loan loan)
        {
            var existingLoan = await _context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.LoanId == loan.LoanId);

            if (existingLoan == null)
            {
                Console.WriteLine("لم يتم العثور على المستعير.");
                return;
            }

            existingLoan.BorrowDate = loan.BorrowDate;
            existingLoan.ReturnDate = loan.ReturnDate;
            existingLoan.BookId = loan.BookId;

            _context.Update(existingLoan);
            await SaveChangesAsync();
            Console.WriteLine("تم تحديث الاستعارة بنجاح.");
        }
        //منحذف عن طريق تاريخ الارجاع اي تم الارجاع بدل من الحذف الكلي من قاعدة البيانات 
        public async Task DeleteAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan != null)
            {
                loan.ReturnDate = DateTime.Now; 
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

      
    }
}
