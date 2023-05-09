using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.DeleteRecipeIngridient
{
    public class DeleteRecipeIngridientCommand : RecipeDto, IRequest
    {
        public int recipeIngridientId { get; set; }
    }
}
