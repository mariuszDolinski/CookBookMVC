using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<PaginatedResult<Recipe>> GetAllRecipes(
            string? searchPhrase, int pageNumber, int pageSize, string[]? ingList, int advancedSearchMode)
        {
            var baseQuery = _dbContext.Recipes
                .Where(r => string.IsNullOrEmpty(searchPhrase) ? true : r.Name.Contains(searchPhrase))
                .OrderByDescending(r => r.CreatedTime);

            List<Recipe> items = new List<Recipe>();
            if(ingList != null && ingList.Length > 0)
            {
                var recipes = await baseQuery
                    .Include(r => r.RecipeIngridients)
                    .ToListAsync();
                int count;
                int searchCount;

                foreach(var r in recipes)
                {
                    if(r.RecipeIngridients.Count == 0) { continue; }
                    count = 0;
                    foreach(var ri in r.RecipeIngridients) 
                    {
                        var name = _dbContext.Ingridients.FirstOrDefault(i => i.Id == ri.IngridientId)!.Name;
                        if(name is null) { continue; }
                        if (ingList.Contains(name))
                        {
                            count++;
                        }
                    }
                    searchCount = (advancedSearchMode == 1) ? ingList.Length : r.RecipeIngridients.Count;
                    if(count == searchCount)
                    {
                        items.Add(r);
                    }
                }
                if(pageNumber == 0)
                {
                    return new PaginatedResult<Recipe>(items, items.Count);
                }
                else
                {
                    return new PaginatedResult<Recipe>(items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(), items.Count);
                }
            }
            if (pageNumber == 0)
            {
                items = await baseQuery.ToListAsync();
            }
            else
            {
                items = await baseQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return new PaginatedResult<Recipe>(items, baseQuery.Count());
        }

        public async Task<Recipe> GetRecipeById(int id)
            => await _dbContext.Recipes
            .Include(r => r.RecipeIngridients)
            .FirstAsync(r => r.Id == id);

        public async Task SaveChangesToDb()
            => await _dbContext.SaveChangesAsync();

        public async Task DeleteById(int id)
        {
            var recipe = await _dbContext.Recipes
                .Include(r => r.RecipeIngridients)
                .FirstAsync(r => r.Id == id);
            _dbContext.Recipes.Remove(recipe);
            await _dbContext.SaveChangesAsync();
        }
    }
}
