using ApplicationData.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationData.Repositories
{
    public class VisitorRepository : IRepository<Visitor>
    {
        private readonly MnagementBdContext _context;

        public VisitorRepository(MnagementBdContext context)
        {
            _context = context;
        }

        public async Task<List<Visitor>> GetAllAsync()
        {
            return await _context.Visitors
                .Include(v => v.Reservations)
                .Include(v => v.Notifications)
      
                .ToListAsync();
        }

        public async Task<Visitor> GetByIdAsync(int visitorId)
        {
            return await _context.Visitors
                .Include(v => v.Reservations)
                .Include(v => v.Notifications)
           
                .FirstOrDefaultAsync(v => v.VisitorId == visitorId);
        }

        public async Task<Visitor> AddAsync(Visitor visitor)
        {
            await _context.Visitors.AddAsync(visitor);
            await SaveChangesAsync();
            return visitor;
        }

        public async Task UpdateAsync(Visitor visitor)
        {
            var existingVisitor = await _context.Visitors
                .Include(v => v.Reservations)
                .Include(v => v.Notifications)
               
                .FirstOrDefaultAsync(v => v.VisitorId == visitor.VisitorId);

            if (existingVisitor == null)
            {
                Console.WriteLine("لم يتم العثور على الزائر.");
                return;
            }

            existingVisitor.Name = visitor.Name;
            existingVisitor.ContactDetails = visitor.ContactDetails;
            existingVisitor.VisitDate = visitor.VisitDate;
            existingVisitor.Reservations = visitor.Reservations;
            existingVisitor.Notifications = visitor.Notifications;
           

            _context.Update(existingVisitor);
            await SaveChangesAsync();
            Console.WriteLine("تم تحديث الزائر بنجاح.");
        }

        public async Task DeleteAsync(int visitorId)
        {
            var visitor = await _context.Visitors.FindAsync(visitorId);
            if (visitor != null)
            {
                _context.Visitors.Remove(visitor);
                await SaveChangesAsync();
            }
        }

        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
