using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationData.Repository
{
    public class ExhibitionRepository : IRepository<Exhibition>
    {
        private readonly MnagementBdContext _context;

        public ExhibitionRepository(MnagementBdContext context)
        {
            _context = context;
        }

        public async Task<Exhibition> GetByIdAsync(int id)
        {
            return await _context.Exhibitions.FindAsync(id); 
        }

        public async Task<List<Exhibition>> GetAllAsync()
        {
            return await _context.Exhibitions.ToListAsync(); 
        }

        public async Task<Exhibition> AddAsync(Exhibition exhibition)
        {
            await _context.Exhibitions.AddAsync(exhibition); 
            await SaveChangesAsync(); 
            return exhibition;
        }

        public async Task UpdateAsync(Exhibition exhibition)
        {
            _context.Exhibitions.Update(exhibition); 
            await SaveChangesAsync(); 
        }

        public async Task DeleteAsync(int id)
        {
            var exhibition = await GetByIdAsync(id); 
            if (exhibition != null)
            {
                _context.Exhibitions.Remove(exhibition);
                await SaveChangesAsync(); 
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();  
        }

        public Exhibition Get(int id)
        {
            return _context.Exhibitions.FirstOrDefault(e=>e.Exhibitionid==id); 
        }

        public IList<Exhibition> GetAll()
        {
            return _context.Exhibitions.ToList(); 
        }

       
        
    }
}
