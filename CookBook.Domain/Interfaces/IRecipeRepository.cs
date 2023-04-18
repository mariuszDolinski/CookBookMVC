using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IRecipeRepository
    {
        Task CreateRecipe(Recipe recipe);
        Task<IEnumerable<Recipe>> GetAllRecipes();
        Task<Recipe> GetRecipeById(int id);
        Task SaveChangesToDb();
    }
}
