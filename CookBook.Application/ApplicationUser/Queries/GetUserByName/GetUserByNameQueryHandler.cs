using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace CookBook.Application.ApplicationUser.Queries.GetUserByName
{
    public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetUserByNameQueryHandler(IUserRepository userRepository, IMapper mapper,
            IUserContext userContext)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<UserDto> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            string userName;
            if (string.IsNullOrEmpty(request.UserName))
            {
                var currentUser = _userContext.GetCurrentUser();
                if(currentUser == null)
                {
                    return new UserDto
                    {
                        UserName = "na"
                    };
                }
                else
                {
                    userName = currentUser.UserName;
                }
            }
            else
            {
                userName = request.UserName;
            }

            var user = await _userRepository.GetUserByUserName(userName);
            if (user == null)
            {
                return new UserDto()
                {
                    UserName = "nn"
                };
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
