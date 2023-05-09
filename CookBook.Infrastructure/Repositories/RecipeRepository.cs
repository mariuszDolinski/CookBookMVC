using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CookBookDbContext _dbContext;

        public RecipeRepository(CookBookDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task CreateRecipe(Recipe recipe)
        {
            _dbContext.Add(recipe);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
            => await _dbContext.Recipes.ToListAsync();

        public async Task<Recipe> GetRecipeById(int id)
            => await _dbContext.Recipes
            .Include(r => r.RecipeIngridients)
            .FirstAsync(r => r.Id == id);

        public async Task SaveChangesToDb()
            => await _dbContext.SaveChangesAsync();

        public async Task AddRecipeIngridient(RecipeIngridient recipeIng)
        {
            _dbContext.RecipeIngridients.Add(recipeIng);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecipeIngridient>> GetAllRecipeIngridients(int recipeId)
            => await _dbContext.RecipeIngridients
                .Where(ri => ri.RecipeId == recipeId)
                .ToListAsync();
    }
}
