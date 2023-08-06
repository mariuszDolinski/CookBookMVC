using Microsoft.AspNetCore.Identity;

namespace CookBook.Domain.Entities;

public partial class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageName { get; set; }
    public bool OnlyForAdults { get; set; } = false;
    public bool IsVegeterian { get; set; } = false;
    public bool IsHidden { get; set; }
    public string? Servings { get; set; }
    public string? Source { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public string AuthorId { get; set; } = default!;
    public IdentityUser Author { get; set; } = default!;

    public int CategoryId { get; set; }
    public virtual RecipeCategory Category { get; set; } = default!;
    

    public List<RecipeIngridient> RecipeIngridients { get; set; } = new List<RecipeIngridient>();
}