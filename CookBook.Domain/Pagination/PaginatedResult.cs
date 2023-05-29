namespace CookBook.Domain.Pagination
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }

        public PaginatedResult(List<T> items, int total) 
        {
            Items = items;
            TotalItems = total;
        }
    }
}
