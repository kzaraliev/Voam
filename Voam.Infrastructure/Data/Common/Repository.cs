using Microsoft.EntityFrameworkCore;
using Voam.Infrastructure.Data;

namespace Voam.Infrastucture.Data.Common
{
    public class Repository : IRepository
    {
        private readonly DbContext context;

        public Repository(VoamDbContext _context)
        {
            context = _context;
        }

        private DbSet<T> DbSet<T>() where T : class 
        {
            return context.Set<T>();
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return DbSet<T>().AsNoTracking();
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await context.AddRangeAsync(entities);
        }

        public void Remove<T>(T entity) where T : class
        {
            DbSet<T>().Remove(entity);
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            DbSet<T>().RemoveRange(entities);
        }

        public async Task<T?> FindAsync<T>(params object[] keyValues) where T : class
        {
            return await DbSet<T>().FindAsync(keyValues);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }        
    }
}
