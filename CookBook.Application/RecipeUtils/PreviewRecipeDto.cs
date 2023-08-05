namespace CookBook.Application.RecipeUtils
{
    public class PreviewRecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool OnlyForAdults { get; set; }
        public bool IsVegeterian { get; set; }
        public bool IsHidden { get; set; }
        public string? ImageName { get; set; }

        public string GetShortName()
        {
            if (Name.Length > 27)
            {
                return Name.Substring(0, 27) + "...";
            }
            return Name;
        }
    }
}
