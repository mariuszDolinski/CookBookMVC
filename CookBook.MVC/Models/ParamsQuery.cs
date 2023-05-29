namespace CookBook.MVC.Models
{
    public class ParamsQuery
    {
        public object? SortOrder { get; set; }
        public object? PageSize { get; set; }
        public object? Page { get; set; }

        public ParamsQuery() 
        {
            SortOrder = "";
            Page = 1;
            PageSize = 5;
        }

        public ParamsQuery(string? sortOrder, int page, int pageSize)
        {
            SortOrder = sortOrder;
            Page = page;
            PageSize = pageSize;
        }
    }
}
