using CookBook.Application.RecipeUtils;
using MediatR;

namespace CookBook.Application.ApplicationUser.Queries.GetAllRecipeCategories
{
    public class GetAllRecipeCategoriesQuery : IRequest<IEnumerable<RecipeCategoryDto>>
    {
    }
}
