using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CookBookDbContext _dbContext;
        public UserRepository(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Recipe>> GetAllUserRecipe(string id)
            => await _dbContext.Recipes.Where(r => r.AuthorId == id).ToListAsync();

        public async Task<IEnumerable<RecipeCategory>> GetAllRecipeCategories()
            => await _dbContext.RecipeCategories.ToListAsync();
    }
}
