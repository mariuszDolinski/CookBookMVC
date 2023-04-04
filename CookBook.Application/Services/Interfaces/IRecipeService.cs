using CookBook.Application.DtoModels;

namespace CookBook.Application.Services
{
    public interface IRecipeService
    {
        Task CreateRecipe(RecipeDto dto);
    }
}