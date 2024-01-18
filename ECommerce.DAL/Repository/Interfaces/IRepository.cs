using ECommerce.Core.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Repository.Interfaces
{
    public interface IRepository<T> where T : BaseAuditableEntity, new() 
    {
        public Task<IQueryable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task Delete(T entity);
        public Task SaveChangesAsync();
        DbSet<T> Table {  get; }
    }
}
