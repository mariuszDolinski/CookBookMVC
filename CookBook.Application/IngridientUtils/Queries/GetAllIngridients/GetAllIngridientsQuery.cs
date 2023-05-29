using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetAllIngridients
{
    public class GetAllIngridientsQuery : IRequest<IEnumerable<IngridientDto>>
    {
        public GetAllIngridientsQuery() 
        {
            SearchPhrase = "";
            SortOrder = "";
            PageNumber = 0;
            PageSize = 0;
        }
        public GetAllIngridientsQuery(string search, string sortOrder, int pageNumber, int pageSize)
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
