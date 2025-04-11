using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetAllIngridientsCategories
{
    public class GetAllIngridientsCategoriesQuery : IRequest<PaginatedResult<IngridientCategoryDto>>
    {
        public string? SearchPhrase { get; set; }
        public string? SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllIngridientsCategoriesQuery()
        {
            SearchPhrase = "";
            SortOrder = "";
            PageNumber = 0;
            PageSize = 0;
        }
    }
}
