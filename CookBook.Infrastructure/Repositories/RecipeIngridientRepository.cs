using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class RecipeIngridientRepository : IRecipeIngridientRepository
    {
        private readonly CookBookDbContext _dbContext;

        public RecipeIngridientRepository(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteRecipeIngridientById(int id)
        {
            var ingridient = _dbContext.RecipeIngridients.First(ri =>  ri.Id == id);
            _dbContext.Remove(ingridient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRecipeIngridient(RecipeIngridient recipeIng)
        {
            _dbContext.RecipeIngridients.Add(recipeIng);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecipeIngridient>> GetAllRecipeIngridients(int recipeId)
            => await _dbContext.RecipeIngridients
                .Where(ri => ri.RecipeId == recipeId)
                .ToListAsync();

        public async Task<RecipeIngridient> GetRecipeIngridientById(int recipeIngId)
            => await _dbContext.RecipeIngridients.FirstAsync(ri => ri.Id == recipeIngId);

        public async Task<RecipeIngridient?> GetRecipeIngridientByIngId(int ingId)
            => await _dbContext.RecipeIngridients.FirstOrDefaultAsync(ri => ri.IngridientId == ingId);
        public async Task<RecipeIngridient?> GetRecipeIngridientByUnitId(int unitId)
            => await _dbContext.RecipeIngridients.FirstOrDefaultAsync(ri => ri.UnitId == unitId);
    }
}
