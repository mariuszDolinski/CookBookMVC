using CookBook.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Recipe>> GetAllUserRecipe(string id);
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<IEnumerable<string>> GetAllUserRoles(AppUser user);
        Task<AppUser?> GetUserByUserName(string username);
    }
}
