namespace CookBook.Domain.Entities;

public partial class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public DateTime? DateOfBirth { get; set; }

    public int RoleId { get; set; }
    public virtual List<Recipe> Recipes { get; } = new List<Recipe>();
    public virtual Role Role { get; set; } = default!;
}
