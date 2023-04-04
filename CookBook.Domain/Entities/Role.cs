namespace CookBook.Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public virtual List<User> Users { get; } = new List<User>();
}
