using AutoMapper;
using CookBook.Application.UnitUtils;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories
{
    public class GetAllRecipeCategoriesQueryHandler :
        IRequestHandler<GetAllRecipeCategoriesQuery, PaginatedResult<RecipeCategoryDto>>
    {
        private readonly IRecipeCategoryRepository _recipeCategoryRepository;
        private readonly IMapper _mapper;

        public GetAllRecipeCategoriesQueryHandler(IRecipeCategoryRepository recipeCategoryRepository, IMapper mapper)
        {
            _recipeCategoryRepository = recipeCategoryRepository;
            _mapper = mapper;        
        }
        public async Task<PaginatedResult<RecipeCategoryDto>> Handle(GetAllRecipeCategoriesQuery request, CancellationToken cancellationToken)
        {
            request.SortOrder ??= "";
            request.SearchPhrase ??= "";
            var paginatedCategories = await _recipeCategoryRepository.GetAll(request.SearchPhrase, request.SortOrder, request.PageNumber, request.PageSize);
            var categoriesDto = _mapper.Map<IEnumerable<RecipeCategoryDto>>(paginatedCategories.Items).ToList();

            return new PaginatedResult<RecipeCategoryDto>(categoriesDto, paginatedCategories.TotalItems);
        }
    }
}
