using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.ApplicationUser.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<string?>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllRolesQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<string?>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllRoles();
        }
    }
}
