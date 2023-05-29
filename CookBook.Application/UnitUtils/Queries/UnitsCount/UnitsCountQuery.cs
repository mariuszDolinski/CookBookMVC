using MediatR;

namespace CookBook.Application.UnitUtils.Queries.UnitsCount
{
    public class UnitsCountQuery : IRequest<int>
    {
        public string SearchPhrase;
        public UnitsCountQuery(string search)
        {
            SearchPhrase = search;
        }
    }
}
