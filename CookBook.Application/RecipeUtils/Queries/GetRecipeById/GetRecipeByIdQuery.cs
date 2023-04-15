using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeById
{
    public class GetRecipeByIdQuery : IRequest<RecipeDto>
    {
        public int Id { get; set; }

        public GetRecipeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
