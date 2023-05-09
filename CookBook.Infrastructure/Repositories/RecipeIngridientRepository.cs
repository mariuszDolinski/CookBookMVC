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
            var ingridient = await _dbContext.RecipeIngridients.FirstAsync(ri =>  ri.Id == id);
            _dbContext.Remove(ingridient);
            await _dbContext.SaveChangesAsync();
        }
    }
}
