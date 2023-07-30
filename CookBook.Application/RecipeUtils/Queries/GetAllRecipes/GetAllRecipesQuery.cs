using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetAllRecipes
{
    public class GetAllRecipesQuery : IRequest<PaginatedResult<PreviewRecipeDto>>
    {
        public GetAllRecipesQuery()
        {
            SearchPhrase = "";
            PageNumber = 0;
            PageSize = 0;
        }
        public GetAllRecipesQuery(string? search, int pageNumber, int pageSize)
        {
            SearchPhrase = string.IsNullOrEmpty(search) ? "" : search;
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 6 ? 6 : pageSize;
        }
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
