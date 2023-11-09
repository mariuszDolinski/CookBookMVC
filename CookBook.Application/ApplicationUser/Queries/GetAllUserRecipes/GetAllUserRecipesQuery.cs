using CookBook.Application.RecipeUtils;
using MediatR;

namespace CookBook.Application.ApplicationUser.Queries.GetAllUserRecipes
{
    public class GetAllUserRecipesQuery : IRequest<IEnumerable<PreviewRecipeDto>>
    {
        public string UserName { get; set; }
        public bool IsCurrentUser { get; set; }

        public GetAllUserRecipesQuery() 
        { 
            UserName = string.Empty;
            IsCurrentUser = true;
        }

        public GetAllUserRecipesQuery(string username, bool isCurrent)
        {
            UserName = username;
            IsCurrentUser = isCurrent;
        }

    }
}
