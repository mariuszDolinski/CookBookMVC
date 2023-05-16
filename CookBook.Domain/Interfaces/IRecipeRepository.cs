using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IRecipeRepository
    {
        Task CreateRecipe(Recipe recipe);
        Task<IEnumerable<Recipe>> GetAllRecipes(string? searchPhrase);
        Task<Recipe> GetRecipeById(int id);
        Task SaveChangesToDb();
        Task DeleteById(int id);
    }
}
