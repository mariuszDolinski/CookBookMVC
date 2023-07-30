using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.AdvancedSearch
{
    public class AdvancedSearchQuery : IRequest<IEnumerable<PreviewRecipeDto>>
    {
        public string[] Ingridients { get; set; }
        public int Mode { get; set; }
        public AdvancedSearchQuery(string[] ings, int mode) 
        {
            Ingridients = ings;
            Mode = mode;
        }
    }
}
