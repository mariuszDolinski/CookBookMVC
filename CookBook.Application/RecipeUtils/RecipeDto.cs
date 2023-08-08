using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Application.RecipeUtils
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool OnlyForAdults { get; set; } = false;
        public bool IsVegeterian { get; set; } = false;
        public bool IsHidden { get; set; } = false;
        public string? Servings { get; set; }
        public string? Source { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string? ImageName { get; set; }
        public string? Author { get; set; }
        public string? CreatedTime { get; set; }
        public bool IsEditable { get; set; }
        public List<RecipeIngridientDto> RecipeIngridients { get; set; } = new List<RecipeIngridientDto>();
        public int CategoryId { get; set; }
    }
}
