namespace Voam.Core
{
    public class PaginationParams
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize 
        {
            get => pageSize;
            set => pageSize = value > MaximumPageSize ? MaximumPageSize : value;
        }

        private int pageSize;
        private const int MaximumPageSize = 6;
    }
}
