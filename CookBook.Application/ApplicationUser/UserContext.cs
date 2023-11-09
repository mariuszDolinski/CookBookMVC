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
        Task<UserDto?> GetUserDetails(string username);
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

        public async Task<AppUser?> GetUserByUserName(string userName)
            => await _user.FindByNameAsync(userName);


        public async Task<UserDto?> GetUserDetails(string username)
        {
            AppUser? user;
            if(username.Length == 0)
            {
                var currentUser = GetCurrentUser();
                if (currentUser is null)
                    return null;
                user = await _user.FindByNameAsync(currentUser.UserName);
            }
            else
            {
                user = await GetUserByUserName(username);
            }

            if (user == null)
                return null;

            UserDto dto = new UserDto()
            {
                UserName = user.UserName ??= "",
                Email = user.Email ??= "",
                PhoneNumber = user.PhoneNumber ??= "",
                LastLogOnTime = user.LastLogOnTime,
                CreatedTime = user.CreatedTime
            };

            return dto;
        }
    }
}
