namespace CookBook.Application.ApplicationUser
{
    public class UserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<string> UserRoles { get; set; } = new List<string>();
    }
}
