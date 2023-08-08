using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using CookBook.Infrastructure.Persistence;
using CookBook.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CookBook.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CookBookDbContext _dbContext;
        private readonly IRepositoryUtils _utils;

        public RecipeRepository(CookBookDbContext dbContext, IRepositoryUtils utils) 
        {
            _dbContext = dbContext;
            _utils = utils;
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
                .Where(r => r.IsHidden == false)
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
                    if(_utils.IsRecipeMeetsSearchCondition(ingList,r.RecipeIngridients,advancedSearchMode,_dbContext))
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
        //public async Task<int> GetCategoryIdByName(string categoryName)
        //{
        //    var category = await _dbContext.RecipeCategories
        //        .FirstOrDefaultAsync(c => c.CategoryName == categoryName);
        //    if(category == null)
        //    {
        //        return 0;
        //    }
        //    return category.CategoryId;
        //}
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

        public async Task<RecipeCategory> GetCategoryById(int id)
            => await _dbContext.RecipeCategories.FirstAsync(r => r.CategoryId == id);
    }
}
