using ApplicationData.Repository;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationData.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly MnagementBdContext _context;
        public BookRepository(MnagementBdContext context)
        {
            _context = context;
        }


        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Author) 
                .Include(b => b.Exhibition) 
                .ToListAsync(); 
        }

        public async Task<Book> GetByIdAsync(int bookId)
        {
            return await _context.Books
                .Include(b => b.Author) 
                .Include(b => b.Exhibition) 
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task AddAsync(Book book)
        {
            
            await _context.Books.AddAsync(book);
            SaveChangesAsync(); 
        }

        
        public async Task UpdateAsync(Book book)
        {
            var existingBook = await _context.Books
                .Include(b => b.Author) 
                .FirstOrDefaultAsync(b => b.Id == book.Id); 

            if (existingBook == null)
            {
                Console.WriteLine("لم يتم العثور على الكتاب.");
                return;
            }

            existingBook.Name = book.Name;
            existingBook.Price = book.Price;
            existingBook.Section = book.Section;
            existingBook.Status = book.Status;
            existingBook.DateOfPublication = book.DateOfPublication;
            existingBook.AuthorId = book.AuthorId;
            existingBook.ExhibitionId = book.ExhibitionId;

            _context.Update(existingBook);
            SaveChangesAsync();
            Console.WriteLine("تم تحديث الكتاب بنجاح.");
        }


       
        public async Task DeleteAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId); 
            if (book != null)
            {
                book.Status = BookStatus.Removed; 
                SaveChangesAsync(); 
            }
        }

        
        public async Task<string> SearchForABookAsync(string bookName, string authorName = null)
        {
            var bookQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(bookName))
            {
                bookQuery = bookQuery.Where(b => b.Name == bookName);
            }

            if (!string.IsNullOrEmpty(authorName))
            {
                bookQuery = bookQuery.Where(b => b.Author.Name == authorName);
            }

            var book = await bookQuery.FirstOrDefaultAsync();

            if (book == null)
            {
                return "الكتاب غير موجود";
            }

            return $"الكتاب موجود: {book.Name}";
        }

        

        Task<Book> IRepository<Book>.AddAsync(Book entity)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();     
        }
    }
}
