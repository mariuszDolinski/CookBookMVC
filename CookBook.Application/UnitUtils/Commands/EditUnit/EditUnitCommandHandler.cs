using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.UnitUtils.Commands.EditUnit
{
    public class EditUnitCommandHandler : IRequestHandler<EditUnitCommand, int>
    {
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IUnitRepository _unitRepository;

        public EditUnitCommandHandler(IUserContext userContext, IMapper mapper,
            IUnitRepository unitRepository)
        {
            _userContext = userContext;
            _mapper = mapper;
            _unitRepository = unitRepository;
        }
        public async Task<int> Handle(EditUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByName(request.OldName);

            if (request.OldName.ToLower() != request.Name!.ToLower())
            {
                var existingUnit = await _unitRepository.GetByName(request.Name);
                if (existingUnit != null)
                {
                    return -1;
                }
            }

            if (unit == null)
            {
                return -2;
            }

            var user = _userContext.GetCurrentUser();
            if (user == null || !(user.IsInRole("Admin") || user.IsInRole("Manager")))
            {
                return 0;
            }

            unit.Name = request.Name!;
            unit.SetEncodedName();
            unit.LastEdit = DateTime.Now;

            await _unitRepository.SaveChangesToDb();
            return 1;
        }
    }
}
