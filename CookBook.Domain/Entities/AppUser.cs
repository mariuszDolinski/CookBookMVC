using Microsoft.AspNetCore.Identity;

namespace CookBook.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastLogOnTime { get; set; }
    }
}
