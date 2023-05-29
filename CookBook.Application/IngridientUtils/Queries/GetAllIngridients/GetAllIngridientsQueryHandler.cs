using AutoMapper;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetAllIngridients
{
    public class GetAllIngridientsQueryHandler : IRequestHandler<GetAllIngridientsQuery, PaginatedResult<IngridientDto>>
    {
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IMapper _mapper;

        public GetAllIngridientsQueryHandler(IIngridientRepository ingridientRepository, IMapper mapper) 
        {
            _ingridientRepository = ingridientRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<IngridientDto>> Handle(GetAllIngridientsQuery request, CancellationToken cancellationToken)
        {
            request.SortOrder ??= "";//jeśli null to wstawiamy pusty string
            request.SearchPhrase ??= "";
            var paginatedIng = await _ingridientRepository.GetAllIngridients(request.SearchPhrase, request.SortOrder, request.PageNumber, request.PageSize);
            
            var ingridientsDto =  _mapper.Map<IEnumerable<IngridientDto>>(paginatedIng.Items).ToList();

            return new PaginatedResult<IngridientDto>(ingridientsDto, paginatedIng.TotalItems);
        }
    }
}
