using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetIngridient
{
    public class GetIngridientQuery : IRequest<int>
    {
        public string? IngName { get; set; }

        public GetIngridientQuery(string name)
        {
            IngName = name;
        }
    }
}
