using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories
{
    public class GetAllRecipeCategoriesQueryHandler :
        IRequestHandler<GetAllRecipeCategoriesQuery, IEnumerable<RecipeCategoryDto>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public GetAllRecipeCategoriesQueryHandler(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;        
        }
        public async Task<IEnumerable<RecipeCategoryDto>> Handle(GetAllRecipeCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _recipeRepository.GetAllRecipeCategories();
            return _mapper.Map<IEnumerable<RecipeCategoryDto>>(categories);
        }
    }
}
