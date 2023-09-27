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

        public UserRepository(CookBookDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Recipe>> GetAllUserRecipe(string id)
            => await _dbContext.Recipes.Where(r => r.AuthorId == id).ToListAsync();

        public async Task<IEnumerable<AppUser>> GetAllUsers()
            => await _userManager.Users.ToListAsync();

        public async Task<IEnumerable<string>> GetAllUserRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<AppUser?> GetUserByUserName(string username)
            => await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}
