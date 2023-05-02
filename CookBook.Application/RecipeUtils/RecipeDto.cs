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
        public IFormFile? ImageFile { get; set; }
        public string? ImageName { get; set; }
        public string? Author { get; set; }
        public string? CreatedTime { get; set; }
        public bool IsEditable { get; set; }
    }
}
