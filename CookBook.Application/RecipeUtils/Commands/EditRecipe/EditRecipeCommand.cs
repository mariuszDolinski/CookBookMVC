using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.EditRecipe
{
    public class EditRecipeCommand : RecipeDto, IRequest
    {
        public int Id { get; set; }
    }
}
