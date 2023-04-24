using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CookBook.Application.ApplicationUser
{
    public interface IUserContext
    {
        CurrentUser GetCurrentUser();
        Task<string?> GetUserNameById(string id);
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        UserManager<IdentityUser> _user;

        public UserContext(IHttpContextAccessor httpContext, UserManager<IdentityUser> user)
        {
            _httpContextAccessor = httpContext;
            _user = user;
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

        public async Task<string?> GetUserNameById(string id)
        {
            var user = await _user.FindByIdAsync(id);
            if (user == null) return null;
            return user.UserName;
        }
    }
}
