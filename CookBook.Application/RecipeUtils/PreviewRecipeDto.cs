namespace CookBook.Application.RecipeUtils
{
    public class PreviewRecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool OnlyForAdults { get; set; } = false;
        public string? ImageName { get; set; }
    }
}
