using Microsoft.AspNetCore.Identity;

namespace CookBook.Application.UnitUtils
{
    public class UnitDto
    {
        public string? Name { get; set; }
        public string? EncodedName { get; private set; }
        public string? CreatedBy { get; set; }
        public string? CreatedTime { get; set; }
    }
}
