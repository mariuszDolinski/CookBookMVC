using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeCategoryById
{
    public class GetRecipeCategoryByIdQuery : IRequest<RecipeCategoryDto>
    {
        public int Id { get; set; }
        public GetRecipeCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
