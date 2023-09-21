using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.DeleteRecipeCategory
{
    public class DeleteRecipeCategoryCommand : RecipeCategoryDto, IRequest<string>
    {
    }
}
