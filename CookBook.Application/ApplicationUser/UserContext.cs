using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CookBook.Application.ApplicationUser
{
    public interface IUserContext
    {
        CurrentUser GetCurrentUser();
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContext)
        {
            _httpContextAccessor = httpContext;
        }
        public CurrentUser GetCurrentUser()
        {
            var user = _httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("Context user doesn't exsists.");
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userName = user.FindFirst(c => c.Type == ClaimTypes.Name)!.Value;

            return new CurrentUser(id, userName);
        }
    }
}
