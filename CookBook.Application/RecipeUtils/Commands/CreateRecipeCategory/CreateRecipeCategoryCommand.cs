using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipeCategory
{
    public class CreateRecipeCategoryCommand : RecipeCategoryDto, IRequest
    {
    }
}
