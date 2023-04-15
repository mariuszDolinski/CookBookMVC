using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipe
{
    public class CreateRecipeCommand : RecipeDto, IRequest
    {
    }
}
