using CookBook.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Domain.Entities;

public partial class Unit : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string EncodedName { get; private set; } = default!;
    public string? CreatedById { get; set; }
    public AppUser? CreatedBy { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public DateTime? LastEdit { get; set; } = DateTime.Now;

    public void SetEncodedName() => EncodedName = Name.ToLower().Replace(" ", "-");
}
