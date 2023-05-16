using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetAllRecipes
{
    public class GetAllRecipesQuery : IRequest<IEnumerable<PreviewRecipeDto>>
    {
        public string? SearchPhrase { get; set; }
    }
}
