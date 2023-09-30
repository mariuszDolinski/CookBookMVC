using MediatR;

namespace CookBook.Application.ApplicationUser.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<IEnumerable<string?>>
    {
    }
}
