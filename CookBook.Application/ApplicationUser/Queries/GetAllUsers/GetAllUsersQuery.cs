using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Application.ApplicationUser.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public string RoleName { get; set; }
        public string UserName { get; set; }

        public GetAllUsersQuery(string roleName, string userName)
        {
            RoleName = roleName;
            UserName = userName;
        }

        public GetAllUsersQuery() 
        {
            RoleName = "";
            UserName = "";
        }
    }
}
