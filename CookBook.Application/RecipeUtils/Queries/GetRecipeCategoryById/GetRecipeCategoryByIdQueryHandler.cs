using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeCategoryById
{
    public class GetRecipeCategoryByIdQueryHandler : IRequestHandler<GetRecipeCategoryByIdQuery, RecipeCategoryDto>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public GetRecipeCategoryByIdQueryHandler(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }
        public async Task<RecipeCategoryDto> Handle(GetRecipeCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _recipeRepository.GetCategoryById(request.Id);
            return _mapper.Map<RecipeCategoryDto>(category);
        }
    }
}
