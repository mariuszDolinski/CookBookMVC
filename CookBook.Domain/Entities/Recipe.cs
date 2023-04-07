namespace CookBook.Domain.Entities;

public partial class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageName { get; set; }
    //public int? AuthorId { get; set; }
    public bool OnlyForAdults { get; set; } = false;
    //public virtual User? Author { get; set; }

    public virtual List<RecipeIngridient> RecipeIngridients { get; } = new List<RecipeIngridient>();
}
