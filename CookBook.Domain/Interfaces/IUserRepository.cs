using CookBook.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Recipe>> GetAllUserRecipe(string id);
        Task<IEnumerable<IdentityUser>> GetAllUsers();
        Task<IEnumerable<string>> GetAllUserRoles(IdentityUser user);
        Task<IdentityUser?> GetUserByUserName(string username);
    }
}
