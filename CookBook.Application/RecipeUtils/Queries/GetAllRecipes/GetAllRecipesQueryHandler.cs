using AutoMapper;
using CookBook.Application.Services.Interfaces;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetAllRecipes
{
    public class GetAllRecipesQueryHandler : IRequestHandler<GetAllRecipesQuery, PaginatedResult<PreviewRecipeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;

        public GetAllRecipesQueryHandler(IMapper mapper, IRecipeRepository recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task<PaginatedResult<PreviewRecipeDto>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            request.SearchPhrase ??= "";
            var paginatedRecipes = await _recipeRepository.GetAllRecipes(request.SearchPhrase, request.PageNumber, request.PageSize, null, 0);

            var dto =  _mapper.Map<IEnumerable<PreviewRecipeDto>>(paginatedRecipes.Items).ToList();

            return new PaginatedResult<PreviewRecipeDto>(dto, paginatedRecipes.TotalItems);
        }
    }}
