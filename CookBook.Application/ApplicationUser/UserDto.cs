using CookBook.Application.RecipeUtils;
using System.Reflection;

namespace CookBook.Application.ApplicationUser
{
    public class UserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? LastLogOnTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        public IEnumerable<string> UserRoles { get; set; } = new List<string>();

        public IEnumerable<PreviewRecipeDto> UserRecipes { get; set; } = new List<PreviewRecipeDto>();

        public static string GetFormattedTime(DateTime? time)
        {
            return (time is not null)
                ? time.Value.Day + "."
                    + time.Value.Month + "."
                    + time.Value.Year + "r."
                : "brak danych";
        }
    }
}
