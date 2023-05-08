using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeIngridients
{
    public class GetRecipeIngridientsQueryHandler
        : IRequestHandler<GetRecipeIngridientsQuery, IEnumerable<RecipeIngridientDto>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public GetRecipeIngridientsQueryHandler(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RecipeIngridientDto>> Handle(GetRecipeIngridientsQuery request, CancellationToken cancellationToken)
        {
            var recipeIngridients = await _recipeRepository.GetAllRecipeIngridients(request.RecipeId);
            return _mapper.Map<IEnumerable<RecipeIngridientDto>>(recipeIngridients);
        }
    }
}
