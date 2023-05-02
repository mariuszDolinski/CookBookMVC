using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CookBook.Application.ApplicationUser
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
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
        public CurrentUser? GetCurrentUser()
        {
            var user = _httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("User doesn't exists in current context.");
            }

            if (user.Identity == null || !user.Identity.IsAuthenticated) 
            {
                return null;
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userName = user.FindFirst(c => c.Type == ClaimTypes.Name)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            return new CurrentUser(id, userName, roles);
        }

        public async Task<string?> GetUserNameById(string id)
        {
            var user = await _user.FindByIdAsync(id);
            if (user == null) return null;
            return user.UserName;
        }
    }
}
