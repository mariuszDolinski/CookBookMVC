using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.AdvancedSearch
{
    public class AdvancedSearchQuery : IRequest<IEnumerable<PreviewRecipeDto>>
    {
        public string SearchName { get; set; }
        public string[]? Ingridients { get; set; }
        public string[]? Categories { get; set; }
        public bool[] Others { get; set; }
        public AdvancedSearchQuery(string phrase, string[]? ings, string[]? cats, bool[] oths) 
        {
            SearchName = phrase;
            Ingridients = ings;
            Categories = cats;
            Others = oths;
        }
    }
}
