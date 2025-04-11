using AutoMapper;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetAllIngridientsCategories
{
    public class GetAllIngridientsCategoriesQueryHandler
        : IRequestHandler<GetAllIngridientsCategoriesQuery, PaginatedResult<IngridientCategoryDto>>
    {
        private readonly IIngridientCategoryRepository _ingCategoryRepository;
        private readonly IMapper _mapper;

        public GetAllIngridientsCategoriesQueryHandler(IIngridientCategoryRepository ingCategoryRepository, IMapper mapper)
        {
            _ingCategoryRepository = ingCategoryRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<IngridientCategoryDto>> Handle(GetAllIngridientsCategoriesQuery request, CancellationToken cancellationToken)
        {
            request.SortOrder ??= "";
            request.SearchPhrase ??= "";
            var paginatedCategories = await _ingCategoryRepository.GetAll(request.SearchPhrase, request.SortOrder, request.PageNumber, request.PageSize);

            var categoriesDto = _mapper.Map<IEnumerable<IngridientCategoryDto>>(paginatedCategories.Items).ToList();

            return new PaginatedResult<IngridientCategoryDto>(categoriesDto, paginatedCategories.TotalItems);
        }
    }
}
