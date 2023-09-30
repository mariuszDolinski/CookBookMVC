using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.ApplicationUser.Commands
{
    public class ChangeUserRolesCommandHandler : IRequestHandler<ChangeUserRolesCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public ChangeUserRolesCommandHandler(IUserRepository userRepository, IUserContext userContext)
        {
            _userRepository = userRepository;
            _userContext = userContext;
        }
        public async Task<string> Handle(ChangeUserRolesCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if(currentUser == null)
            {
                return "Użytkownik nieautoryzowany";
            }

            if(currentUser.UserName == request.UserName)
            {
                return "Operacja niedozwolona! Administrator nie może sam sobie zmienić ról.";
            }

            return await _userRepository.ChangeUserRoles(request.UserName, request.Roles);
        }
    }
}
