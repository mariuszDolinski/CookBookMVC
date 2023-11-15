using AutoMapper;
using CookBook.Application.IngridientUtils;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.UnitUtils.Queries.GetAllUserUnits
{
    public class GetAllUserUnitsQueryHandler : IRequestHandler<GetAllUserUnitsQuery, IEnumerable<UnitDto>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public GetAllUserUnitsQueryHandler(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UnitDto>> Handle(GetAllUserUnitsQuery request, CancellationToken cancellationToken)
        {
            var units = await _unitRepository.GetAllUserUnits(request.UserName);

            return _mapper.Map<IEnumerable<UnitDto>>(units).ToList();
        }
    }
}
