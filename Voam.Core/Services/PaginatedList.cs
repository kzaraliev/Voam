namespace Voam.Core.Services
{
    public class PaginatedList<T>
    {
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<T> Items { get; set; }

        public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            Items = items;
            TotalPages = (int)Math.Ceiling(count/ (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
        }
    }
}
