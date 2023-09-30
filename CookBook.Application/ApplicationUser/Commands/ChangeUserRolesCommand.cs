using MediatR;

namespace CookBook.Application.ApplicationUser.Commands
{
    public class ChangeUserRolesCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set;}

        public ChangeUserRolesCommand() 
        { 
            UserName = string.Empty; 
            Roles = new List<string>();
        }

        public ChangeUserRolesCommand(string userName, List<string> roles)
        {
            UserName = userName;
            Roles = roles;
        }
    }
}
