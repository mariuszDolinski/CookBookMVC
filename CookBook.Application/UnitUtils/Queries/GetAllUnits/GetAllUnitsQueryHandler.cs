using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.UnitUtils.Queries.GetAllUnits
{
    public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, IEnumerable<UnitDto>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public GetAllUnitsQueryHandler(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnitDto>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            var units = await _unitRepository.GetAllUnits();
            return _mapper.Map<IEnumerable<UnitDto>>(units);
        }
    }
}
