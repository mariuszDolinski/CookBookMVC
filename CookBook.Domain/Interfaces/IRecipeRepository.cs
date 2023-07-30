using CookBook.Domain.Entities;
using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface IRecipeRepository
    {
        Task CreateRecipe(Recipe recipe);
        Task<PaginatedResult<Recipe>> GetAllRecipes(string? searchPhrase, int pageNumber, int pageSize, string[]? ingList, int advancedSearchMode);
        Task<Recipe> GetRecipeById(int id);
        Task SaveChangesToDb();
        Task DeleteById(int id);
    }
}
