using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Recipe>> GetAllUserRecipe(string id);
        Task<IEnumerable<RecipeCategory>> GetAllRecipeCategories();
    }
}
