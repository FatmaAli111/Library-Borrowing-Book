using Infrastructure.Context;
using Infrastructure.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public GenericRepository(AppDbContext Context)
        {
            context = Context;
        }


        public async Task AddAsync(T entity)
        {
         await context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(ICollection<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);

        }

        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        public void Commit()
        {
             context.Database.CommitTransaction();
        }

        public async Task DeleteAsync(T entity)
        {
             context.Set<T>().Remove(entity);
        }

        public async Task DeleteRangeAsync(ICollection<T> entities)
        {
            context.Set<T>().RemoveRange(entities);

        }

        public async Task<T> GetByIdAsync(int id)
        {
            var Result = await context.Set<T>().FindAsync(id);
            return Result;
        }

        public IQueryable<T> GetTableAsTracking()
        {

            var Result = context.Set<T>().AsTracking().AsQueryable();
            return Result;
        }

        public IQueryable<T> GetTableNoTracking()
        {
            var Result = context.Set<T>().AsNoTracking().AsQueryable();
            return Result;
        }

        public void RollBack()
        {

            context.Database.RollbackTransaction();

        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
             context.Set<T>().Update(entity);
        }

        public async Task UpdateRangeAsync(ICollection<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }
    }
}
