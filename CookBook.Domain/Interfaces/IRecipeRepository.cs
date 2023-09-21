using CookBook.Domain.Entities;
using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface IRecipeRepository : IRepository
    {
        Task CreateRecipe(Recipe recipe);
        Task<PaginatedResult<Recipe>> GetAllRecipes(string? searchPhrase, int pageNumber, int pageSize);
        Task<IEnumerable<Recipe>> GetAllFilteredRecipes(string[]? ingridients, string[]? categories, bool[] othersFilters);
        Task<Recipe> GetRecipeById(int id);
        Task SaveChangesToDb();
        Task DeleteById(int id);
    }
}
