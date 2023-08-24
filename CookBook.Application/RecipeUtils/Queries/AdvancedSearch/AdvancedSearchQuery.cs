using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.AdvancedSearch
{
    public class AdvancedSearchQuery : IRequest<IEnumerable<PreviewRecipeDto>>
    {
        public string[]? Ingridients { get; set; }
        public string[]? Categories { get; set; }
        public bool[] Others { get; set; }
        public AdvancedSearchQuery(string[]? ings, string[]? cats, bool[] oths) 
        {
            Ingridients = ings;
            Categories = cats;
            Others = oths;
        }
    }
}
