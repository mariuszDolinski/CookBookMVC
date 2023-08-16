namespace CookBook.Domain.Utils
{
    public class RecipesQueryParams
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int AdvancedSearchMode { get; set; }
        public string[]? IngList { get; set; }
        public string Category { get; set; } = default!;
        public bool IsVegetarian { get; set; }
    }
}
