using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Application.ApplicationUser.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            foreach (var user in usersDto)
            {
                var currentUser = await _userRepository.GetUserByUserName(user.UserName);
                if (currentUser is not null)
                {
                    user.UserRoles = await _userRepository.GetAllUserRoles(currentUser);
                }               
            }

            return usersDto;
        }
    }
}
