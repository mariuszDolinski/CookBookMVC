using AutoMapper;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using MediatR;

namespace CookBook.Application.UnitUtils.Queries.GetAllUnits
{
    public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, PaginatedResult<UnitDto>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public GetAllUnitsQueryHandler(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UnitDto>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            request.SortOrder ??= "";
            request.SearchPhrase ??= "";
            var paginatedUnits = await _unitRepository.GetAllUnits(request.SearchPhrase, request.SortOrder, request.PageNumber, request.PageSize);
            var unitsDto = _mapper.Map<IEnumerable<UnitDto>>(paginatedUnits.Items).ToList();

            return new PaginatedResult<UnitDto>(unitsDto, paginatedUnits.TotalItems);
        }
    }
}
