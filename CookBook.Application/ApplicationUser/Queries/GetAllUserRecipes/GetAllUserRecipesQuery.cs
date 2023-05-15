using CookBook.Application.RecipeUtils;
using MediatR;

namespace CookBook.Application.ApplicationUser.Queries.GetAllUserRecipes
{
    public class GetAllUserRecipesQuery : IRequest<IEnumerable<PreviewRecipeDto>>
    {
    }
}
