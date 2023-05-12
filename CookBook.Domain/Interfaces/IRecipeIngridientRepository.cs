using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IRecipeIngridientRepository
    {
        Task AddRecipeIngridient(RecipeIngridient recipeIng);
        Task<IEnumerable<RecipeIngridient>> GetAllRecipeIngridients(int recipeId);
        Task<RecipeIngridient> GetRecipeIngridientById(int recipeIngId);
        Task DeleteRecipeIngridientById(int id);
    }
}
