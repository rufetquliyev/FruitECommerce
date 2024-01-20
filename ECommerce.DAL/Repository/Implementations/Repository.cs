using ECommerce.Core.Common;
using ECommerce.DAL.Context;
using ECommerce.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseAuditableEntity, new()
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
            entity.CreatedAt = DateTime.Now;
        }

        public async Task Delete(T entity)
        {
            entity.IsDeleted = true;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return Table.Where(x => !x.IsDeleted);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Table.Update(entity);
        }
    }
}
