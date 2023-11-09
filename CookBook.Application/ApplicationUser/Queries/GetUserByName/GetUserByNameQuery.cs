using MediatR;

namespace CookBook.Application.ApplicationUser.Queries.GetUserByName
{
    public class GetUserByNameQuery : IRequest<UserDto>
    {
        public string UserName { get; set; }

        public GetUserByNameQuery()
        {
            UserName = string.Empty;
        }

        public GetUserByNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
