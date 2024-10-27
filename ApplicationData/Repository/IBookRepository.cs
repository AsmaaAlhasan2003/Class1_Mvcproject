using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationData.Repository
{
    
        public interface IBookRepository : IRepository<Book>
        {
            Book AddBook(Book book);
            void RemoveBook(int id);
            Book UpdateAuthorName(int bookId, string newAuthorName);
            IList<Book> GetAllBooks();
            Book GetBookById(int bookId);
            bool IsBookExists(int bookId);
            string SearchForABook(string bookName, string authorName = null);
        }
    }


