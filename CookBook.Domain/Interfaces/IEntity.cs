using CookBook.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Domain.Interfaces
{
    public interface IEntity
    {
        string Name { get; set; }
        DateTime CreatedTime { get; set; }
        AppUser? CreatedBy { get; set; }
    }
}
