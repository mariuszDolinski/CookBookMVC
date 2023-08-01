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

            #region advanced search mode
            if (ingList != null && ingList.Length > 0)
            {
                var recipes = await baseQuery
                    .Include(r => r.RecipeIngridients)
                    .ToListAsync();

                foreach(var r in recipes)
                {
                    if(IsRecipeMeetsSearchCondition(ingList,r.RecipeIngridients,advancedSearchMode))
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
            #endregion

            #region regular mode
            if (pageNumber == 0)
            {
                items = await baseQuery.ToListAsync();
            }
            else
            {
                items = await baseQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return new PaginatedResult<Recipe>(items, baseQuery.Count());
            #endregion
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

        #region Utilities
        private bool IsRecipeMeetsSearchCondition
            (string[] searchList, List<RecipeIngridient> recipeIngs, int mode)
        {
            if (recipeIngs.Count == 0) { return false; }

            switch (mode)
            {
                case 1:
                    foreach(var ing in searchList)
                    {
                        bool isOnTheList = false;
                        foreach(var ri in recipeIngs)
                        {
                            var name = _dbContext.Ingridients.FirstOrDefault(i => i.Id == ri.IngridientId)!.Name;
                            if (name != null && name == ing)
                            {
                                isOnTheList = true;
                                break;
                            }
                        }
                        if (!isOnTheList)
                        {
                            return false;
                        }
                    }
                    break;
                case 2:
                    foreach (var ri in recipeIngs)
                    {
                        var name = _dbContext.Ingridients.FirstOrDefault(i => i.Id == ri.IngridientId)!.Name;
                        if (!searchList.Contains(name))
                        {
                            return false;
                        }
                    }
                    break;
                default: return false;

            }

            return true;
        }
        #endregion
    }
}
