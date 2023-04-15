using Microsoft.AspNetCore.Http;

namespace CookBook.Application.RecipeUtils
{
    public class RecipeDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool OnlyForAdults { get; set; } = false;
        public IFormFile? ImageFile { get; set; }
        public string? ImageName { get; set; }
    }
}
