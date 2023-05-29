namespace CookBook.Application.Pagination
{
    public class PaginationResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; }

        public PaginationResult(List<T> items, int total) 
        {
            Items = items;
            TotalItems = total;
        }
    }
}
