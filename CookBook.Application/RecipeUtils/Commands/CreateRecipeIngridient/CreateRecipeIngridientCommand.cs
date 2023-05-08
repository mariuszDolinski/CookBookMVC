using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipeIngridient
{
    public class CreateRecipeIngridientCommand : RecipeIngridientDto, IRequest<bool>
    {
        public int RecipeId { get; set; } = default!;
    }
}
