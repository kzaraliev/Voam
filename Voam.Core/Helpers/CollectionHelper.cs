using Microsoft.EntityFrameworkCore;
using Voam.Core.Services;

namespace Voam.Core.Helpers
{
    public class CollectionHelper<T>
    {
        public static async Task<PaginatedList<T>> ToPaginatedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
