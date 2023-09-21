using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.UnitUtils.Commands.DeleteUnits
{
    public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, string>
    {
        private readonly IUserContext _userContext;
        private readonly IUnitRepository _unitRepository;
        private readonly IRecipeIngridientRepository _riRepository;

        public DeleteUnitCommandHandler(IUserContext userContext,
            IUnitRepository unitRepository, IRecipeIngridientRepository riRepository)
        {
            _userContext = userContext;
            _unitRepository = unitRepository;
            _riRepository = riRepository;
        }
        public async Task<string> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            if (user == null || (!user.IsInRole("Admin") && !user.IsInRole("Manager")))
            {
                return "Brak uprawnień do usunięcia jednostki";
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return "Wybierz jednostkę do usunięcia";
            }

            var unit = await _unitRepository.GetByName(request.Name);
            if (unit == null)
            {
                return "Jednostka, którą próbujesz usunąć, nie istnieje";
            }

            var recipeIng = await _riRepository.GetRecipeIngridientByUnitId(unit.Id);
            if (recipeIng != null)
            {
                return "Nie można usunąć jednostki, gdyż jest już częścią jakiegoś przepisu";
            }

            await _unitRepository.DeleteById(unit.Id);
            return "";

        }
    }
}
