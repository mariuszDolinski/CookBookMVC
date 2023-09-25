using AutoMapper;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.AdvancedSearch
{
    public class AdvancedSearchQueryHandler : IRequestHandler<AdvancedSearchQuery, IEnumerable<PreviewRecipeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;

        public AdvancedSearchQueryHandler(IMapper mapper, IRecipeRepository recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task<IEnumerable<PreviewRecipeDto>> Handle(AdvancedSearchQuery request, CancellationToken cancellationToken)
        {
            var result = await _recipeRepository.GetAllFilteredRecipes
                (request.SearchName, request.Ingridients, request.Categories, request.Others);

            var dto = _mapper.Map<IEnumerable<PreviewRecipeDto>>(result).ToList();

            return dto;
        }
    }
}
