namespace CookBook.Domain.Entities;

public partial class Ingridient
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string EncodedName { get; private set; } = default!;

    public void SetEncodedName() => EncodedName = Name.ToLower().Replace(" ","-");
}
