using Microsoft.AspNetCore.Identity;

namespace CookBook.Domain.Entities;

public partial class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageName { get; set; }
    public bool OnlyForAdults { get; set; } = false;

    public string AuthorId { get; set; } = default!;
    public IdentityUser Author { get; set; } = default!;
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public List<RecipeIngridient> RecipeIngridients { get; set; } = new List<RecipeIngridient>();
}