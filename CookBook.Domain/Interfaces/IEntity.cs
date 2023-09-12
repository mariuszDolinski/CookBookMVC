using Microsoft.AspNetCore.Identity;

namespace CookBook.Domain.Interfaces
{
    public interface IEntity
    {
        string Name { get; set; }
        DateTime CreatedTime { get; set; }
        IdentityUser? CreatedBy { get; set; }
    }
}
