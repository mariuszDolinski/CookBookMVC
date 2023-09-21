using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.UnitUtils.Commands.EditUnit
{
    public class EditUnitCommandHandler : IRequestHandler<EditUnitCommand, string>
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
        public async Task<string> Handle(EditUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByName(request.OldName);

            if (request.OldName.ToLower() != request.Name!.ToLower())
            {
                var existingUnit = await _unitRepository.GetByName(request.Name);
                if (existingUnit != null)
                {
                    return "Jednostka o podanej nazwie już istnieje";
                }
            }

            if (unit == null)
            {
                return "Edycja jednostki nie powiodła się.";
            }

            var user = _userContext.GetCurrentUser();
            if (user == null || !(user.IsInRole("Admin") || user.IsInRole("Manager")))
            {
                return "Brak uprawnień do edycji jednostki";
            }

            unit.Name = request.Name!;
            unit.SetEncodedName();
            unit.LastEdit = DateTime.Now;

            await _unitRepository.SaveChangesToDb();
            return "";
        }
    }
}
