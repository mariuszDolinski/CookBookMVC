using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Application.IngridientUtils
{
    public class IngridientDto
    {
        public string? Name { get; set; }
        public string? EncodedName { get; private set; }
        public string? CreatedBy { get; set; }
        public string? CreatedTime { get; set; }
    }
}
