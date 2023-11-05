using CookBook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CookBook.Application.ApplicationUser
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
        Task<string?> GetUserNameById(string id);
        Task<UserDto?> GetCurrentUserDetails();
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        UserManager<AppUser> _user;

        public UserContext(IHttpContextAccessor httpContext, UserManager<AppUser> user)
        {
            _httpContextAccessor = httpContext;
            _user = user;
        }
        public CurrentUser? GetCurrentUser()
        {
            var user = _httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("Nie znaleziono użytkownika");
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

        public async Task<UserDto?> GetCurrentUserDetails()
        {
            var currentUser = GetCurrentUser();
            if (currentUser is null)
                return null;
            var user = await _user.FindByNameAsync(currentUser.UserName);
            if (user == null) 
                return null;
            UserDto dto = new UserDto()
            {
                UserName = currentUser.UserName ??= "",
                Email = user.Email ??= "",
                PhoneNumber = user.PhoneNumber ??= "",
                UserRoles = currentUser.Roles
            };

            return dto;
        }
    }
}
