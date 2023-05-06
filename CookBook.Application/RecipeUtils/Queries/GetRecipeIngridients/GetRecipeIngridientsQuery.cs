using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeIngridients
{
    public class GetRecipeIngridientsQuery : IRequest<RecipeIngridientDto>
    {
        public int RecipeId { get; set; }
    }
}
