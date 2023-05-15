using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.DeleteRecipe
{
    public class DeleteRecipeCommand : RecipeDto, IRequest
    {
        public DeleteRecipeCommand(int id)
        {
            Id = id;
        }
    }
}
