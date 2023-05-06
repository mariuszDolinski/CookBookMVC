using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.UnitUtils.Commands.CreateUnit
{
    public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IUnitRepository _unitRepository;

        public CreateUnitCommandHandler(IUserContext userContext, IMapper mapper,
            IUnitRepository unitRepository)
        {
            _userContext = userContext;
            _mapper = mapper;
            _unitRepository = unitRepository;
        }
        public async Task Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = _mapper.Map<Domain.Entities.Unit>(request);
            unit.SetEncodedName();
            unit.CreatedById = _userContext.GetCurrentUser()!.Id;
            unit.CreatedTime = DateTime.Now;
            await _unitRepository.CreateUnit(unit);
        }
    }
}
