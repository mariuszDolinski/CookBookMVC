using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeIngridients
{
    public class GetRecipeIngridientsQuery : IRequest<IEnumerable<RecipeIngridientDto>>
    {
        public int RecipeId { get; set; }
    }
}
