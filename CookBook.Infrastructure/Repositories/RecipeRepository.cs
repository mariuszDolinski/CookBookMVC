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
            string? searchPhrase, int pageNumber, int pageSize)
        {
            var baseQuery = _dbContext.Recipes
                .Where(r => r.IsHidden == false)
                .Where(r => string.IsNullOrEmpty(searchPhrase) ? true : r.Name.Contains(searchPhrase))
                .OrderByDescending(r => r.CreatedTime);

            List<Recipe> items = new List<Recipe>();

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

        public async Task<IEnumerable<Recipe>> GetAllFilteredRecipes(
            string[]? ingridients, string[]? categories, bool[] othersFilters)
        {
            var baseQuery = _dbContext.Recipes
                .Include(r => r.Category)
                .Where(r => r.IsHidden == false);

            //filtrujemy po kategoriach
            if(categories != null)
            {
                baseQuery = baseQuery
                    .Where(r => categories.Contains(r.Category.CategoryName));                   
            }

            //pozostałe filtry:
            //0-isVege
            if (othersFilters[0])
                baseQuery = baseQuery.Where(r => r.IsVegeterian);

            var filteredQuery = baseQuery.OrderByDescending(r => r.CreatedTime);

            List<Recipe> items = new List<Recipe>();

            //wyszukiwanie po składnikach
            if (ingridients != null)
            {
                var recipes = await filteredQuery
                    .Include(r => r.RecipeIngridients)
                    .ToListAsync();

                int advancedSearchMode = int.Parse(ingridients[0]);
                string[] ingList = new string[ingridients.Length-1];
                for (int i = 1; i < ingridients.Length; i++)
                    ingList[i - 1] = ingridients[i];

                foreach (var r in recipes)
                {
                    if (_utils.IsRecipeMeetsSearchCondition(ingList, r.RecipeIngridients, advancedSearchMode, _dbContext))
                    {
                        items.Add(r);
                    }
                }
            }
            else
            {
                items = await filteredQuery.ToListAsync();
            }

            return items;
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

        public async Task<IEnumerable<RecipeCategory>> GetAllRecipeCategories()
            => await _dbContext.RecipeCategories
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        public async Task<RecipeCategory> GetCategoryById(int id)
            => await _dbContext.RecipeCategories.FirstAsync(r => r.CategoryId == id);

        public async Task<RecipeCategory?> GetCategoryByName(string name)
            => await _dbContext.RecipeCategories.FirstOrDefaultAsync(r => r.CategoryName == name);

        public async Task CreateCategory(RecipeCategory category)
        {
            _dbContext.Add(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
