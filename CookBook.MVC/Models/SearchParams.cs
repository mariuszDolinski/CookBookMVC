namespace CookBook.MVC.Models
{
    public class SearchParams
    {
        public string? IngridientsList { get; set; }
        public int Mode { get; set; }
        public string? ChoosenCategories { get; set; }
        public string? OtherFilters { get; set; }

        public SearchParams(string ingList, int mode, string choosenCategories, string otherFilters)
        {
            IngridientsList = ingList;
            Mode = mode;
            ChoosenCategories = choosenCategories;
            OtherFilters = otherFilters;
        }

        public SearchParams() { }
    }
}
