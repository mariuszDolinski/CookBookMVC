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
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(CookBookDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Recipe>> GetAllUserRecipe(string id)
            => await _dbContext.Recipes.Where(r => r.AuthorId == id).ToListAsync();

        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
            => await _userManager.Users.ToListAsync();

        public async Task<IEnumerable<string>> GetAllUserRoles(IdentityUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityUser?> GetUserByUserName(string username)
            => await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}
