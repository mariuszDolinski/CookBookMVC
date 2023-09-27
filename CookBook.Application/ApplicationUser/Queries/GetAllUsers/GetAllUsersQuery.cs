using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Application.ApplicationUser.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}
