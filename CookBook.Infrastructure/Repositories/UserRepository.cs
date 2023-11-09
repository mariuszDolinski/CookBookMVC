using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CookBook.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CookBookDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(CookBookDbContext dbContext, UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<Recipe>> GetAllUserRecipes(string userName, bool isCurrent)
            => (isCurrent) 
                ? await _dbContext.Recipes
                    .Where(r => r.Author.UserName == userName)
                    .ToListAsync()
                : await _dbContext.Recipes
                    .Where(r => r.Author.UserName == userName)
                    .Where(r => !r.IsHidden)
                    .ToListAsync();

        public async Task<IEnumerable<AppUser>> GetAllUsers(string roleName, string userName)
        {
            IEnumerable<AppUser> users;
            if(string.IsNullOrEmpty(roleName))
            {
                users = await _userManager.Users.ToListAsync();
            }
            else
            {
                users = await _userManager.GetUsersInRoleAsync(roleName);
            }

            if(!string.IsNullOrEmpty(userName))
            {
                users = users.Where(u => u.UserName!.Contains(userName)).ToList();
            }

            return users;
            
        }       

        public async Task<AppUser?> GetUserByUserName(string username)
            => await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

        public async Task<IEnumerable<string>> GetAllUserRoles(AppUser user)
            => await _userManager.GetRolesAsync(user);

        public async Task<IEnumerable<string?>> GetAllRoles()
            => await _roleManager.Roles
                .Select(r => r.Name).ToListAsync();

        public async Task<string> ChangeUserRoles(string userName, List<string> newRoles)
        {
            var user = await GetUserByUserName(userName); 
            if(user == null) 
            {
                return "Nieznany użytkownik";
            }

            var roles = await GetAllRoles();
            foreach(var role in roles)
            {
                if (newRoles.Contains(role!))
                {
                    try
                    {
                        await _userManager.AddToRoleAsync(user, role!);
                    }catch (Exception ex)
                    {
                        return ex.Message;
                    }
                    
                }
                else
                {
                    try
                    {
                        await _userManager.RemoveFromRoleAsync(user, role!);
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }

            return "";
        }
       
    }
}
