namespace CookBook.Application.RecipeUtils
{
    public class RecipeIndexDto
    {
        public IEnumerable<PreviewRecipeDto> Recipes { get; set; } = new List<PreviewRecipeDto>();
        public bool IsInSearchMode { get; set; }
    }
}
