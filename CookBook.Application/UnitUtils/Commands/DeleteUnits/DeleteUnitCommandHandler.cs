using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.UnitUtils.Commands.DeleteUnits
{
    public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, int>
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
        public async Task<int> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            if (user == null || (!user.IsInRole("Admin") && !user.IsInRole("Manager")))
            {
                return -1;
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return -2;
            }

            var unit = await _unitRepository.GetByName(request.Name);
            if (unit == null)
            {
                return -3;
            }

            var recipeIng = await _riRepository.GetRecipeIngridientByUnitId(unit.Id);
            if (recipeIng != null)
            {
                return 0;
            }

            await _unitRepository.DeleteById(unit.Id);
            return 1;

        }
    }
}
