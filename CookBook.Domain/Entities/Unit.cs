namespace CookBook.Domain.Entities;

public partial class Unit
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string EncodedName { get; private set; } = default!;

    public void SetEncodedName() => EncodedName = Name.ToLower().Replace(" ", "-");
}
