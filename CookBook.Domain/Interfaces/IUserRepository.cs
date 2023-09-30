using CookBook.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Recipe>> GetAllUserRecipes(string id);
        Task<IEnumerable<AppUser>> GetAllUsers(string roleName, string userName);
        Task<AppUser?> GetUserByUserName(string username);
        Task<IEnumerable<string>> GetAllUserRoles(AppUser user);
        Task<IEnumerable<string?>> GetAllRoles();
        Task<string> ChangeUserRoles(string userName, List<string> newRoles);
    }
}
