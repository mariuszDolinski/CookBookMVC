using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.EditRecipeIngridient
{
    public class EditRecipeIngridientCommand : RecipeIngridientDto, IRequest<bool>
    {
        public int RecipeId { get; set; }
    }
}
