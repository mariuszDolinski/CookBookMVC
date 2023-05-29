using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.UnitUtils.Queries.GetAllUnits
{
    public class GetAllUnitsQuery : IRequest<PaginatedResult<UnitDto>>
    {
        public GetAllUnitsQuery()
        {
            SearchPhrase = "";
            SortOrder = "";
            PageNumber = 0;
            PageSize = 0;
        }
        public GetAllUnitsQuery(string search, string sortOrder, int pageNumber, int pageSize)
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
