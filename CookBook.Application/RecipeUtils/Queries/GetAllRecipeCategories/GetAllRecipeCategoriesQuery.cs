using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories
{
    public class GetAllRecipeCategoriesQuery : IRequest<PaginatedResult<RecipeCategoryDto>>
    {
        public GetAllRecipeCategoriesQuery()
        {
            SearchPhrase = "";
            SortOrder = "";
            PageNumber = 0;
            PageSize = 0;
        }
        public GetAllRecipeCategoriesQuery(string search, string sortOrder, int pageNumber, int pageSize)
        {
            SearchPhrase = search;
            SortOrder = sortOrder;
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 5 ? 5 : pageSize;
        }

        public string? SearchPhrase { get; set; }
        public string? SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
