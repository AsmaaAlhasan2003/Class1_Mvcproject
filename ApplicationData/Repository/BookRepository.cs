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
                .ToListAsync(); 
        }

        public async Task<Book> GetByIdAsync(int bookId)
        {
            return await _context.Books
                .Include(b => b.Author)
                
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<Book> AddAsync(Book book)
        {
            
            await _context.Books.AddAsync(book);
           await SaveChangesAsync();
            return book;
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
           

            _context.Update(existingBook);
           await SaveChangesAsync();
            Console.WriteLine("تم تحديث الكتاب بنجاح.");
        }


       
        public async Task DeleteAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId); 
            if (book != null)
            {
                book.Status = BookStatus.Removed; 
             await   SaveChangesAsync(); 
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

       

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();     
        }
    }
}
