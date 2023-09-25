namespace CookBook.MVC.Models
{
    public class SearchParams
    {
        public string SearchName { get; set; } = "";
        public string? IngridientsList { get; set; }
        public int Mode { get; set; }
        public string? ChoosenCategories { get; set; }
        public string? OtherFilters { get; set; }

        public SearchParams(string searchname, string ingList, int mode, 
            string choosenCategories, string otherFilters)
        {
            SearchName = searchname;
            IngridientsList = ingList;
            Mode = mode;
            ChoosenCategories = choosenCategories;
            OtherFilters = otherFilters;
        }

        public SearchParams() { }
    }
}
