namespace Voam.Infrastucture.Data.Common
{
    public interface IRepository
    {
        IQueryable<T> All<T>() where T : class;
        IQueryable<T> AllReadOnly<T>() where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;
        void Remove<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entities) where T : class;
        Task<T?> FindAsync<T>(params object[] keyValues) where T : class;
        Task<int> SaveChangesAsync();
    }
}
