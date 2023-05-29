using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.UnitUtils.Queries.UnitsCount
{
    public class UnitsCountQueryHandler : IRequestHandler<UnitsCountQuery, int>
    {
        private readonly IUnitRepository _unitRepository;

        public UnitsCountQueryHandler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }
        public async Task<int> Handle(UnitsCountQuery request, CancellationToken cancellationToken)
        {
            return await _unitRepository.GetCount(request.SearchPhrase);
        }
    }
}
