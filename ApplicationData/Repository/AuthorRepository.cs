using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationData.Repository
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly MnagementBdContext _context;

        public AuthorRepository(MnagementBdContext context)
        {
            _context = context;
        }

        public async Task<Author> AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author); 
            await SaveChangesAsync(); 
            return author;
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author); 
            await SaveChangesAsync(); 
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync(); 
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await GetByIdAsync(id); 
            if (author != null)
            {
                _context.Authors.Remove(author); 
                await SaveChangesAsync(); 
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync(); 
        }

       

    }
}
