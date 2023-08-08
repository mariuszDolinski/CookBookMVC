using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories
{
    public class GetAllRecipeCategoriesQuery : IRequest<IEnumerable<RecipeCategoryDto>>
    {
    }
}
