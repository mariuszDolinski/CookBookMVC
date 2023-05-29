using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.IngridientsCount
{
    public class IngridientsCountQuery : IRequest<int>
    {
        public string SearchPhrase;
        public IngridientsCountQuery(string search) 
        { 
            SearchPhrase = search;
        }
    }
}
