namespace CookBook.Domain.Entities;

public partial class RecipeIngridient
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int IngridientId { get; set; }
    public int UnitId { get; set; }
    public string Description { get; set; } = default!;

    public int RecipeId { get; set; }
    public virtual Recipe Recipe { get; set; } = default!;
}
