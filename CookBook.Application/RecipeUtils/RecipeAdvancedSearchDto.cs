namespace CookBook.Application.RecipeUtils
{
    public class RecipeAdvancedSearchDto
    {
        public IEnumerable<PreviewRecipeDto> Recipes { get; set; } = new List<PreviewRecipeDto>();
        public string[] IngridientsToSearch { get; set; } = default!;
        public int Mode { get; set; }
    }
}
