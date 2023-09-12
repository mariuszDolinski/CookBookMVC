using CookBook.Domain.Entities;
using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface IRecipeRepository
    {
        Task CreateRecipe(Recipe recipe);
        Task<PaginatedResult<Recipe>> GetAllRecipes(string? searchPhrase, int pageNumber, int pageSize);
        Task<IEnumerable<Recipe>> GetAllFilteredRecipes(string[]? ingridients, string[]? categories, bool[] othersFilters);
        Task<Recipe> GetRecipeById(int id);
        Task<PaginatedResult<RecipeCategory>> GetAllRecipeCategories(string searchPhrase, string sortOrder, int pageNumber, int pageSize);
        Task<RecipeCategory> GetCategoryById(int id);
        Task<RecipeCategory?> GetCategoryByName(string name);
        Task CreateCategory(RecipeCategory category);
        Task SaveChangesToDb();
        Task DeleteById(int id);
    }
}
