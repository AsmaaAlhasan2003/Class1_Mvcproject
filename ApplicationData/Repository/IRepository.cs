using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationData.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        
        Task UpdateAsync(T entity);

        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();


        Task DeleteAsync(int id);

   
        Task SaveChangesAsync();
    }
}
