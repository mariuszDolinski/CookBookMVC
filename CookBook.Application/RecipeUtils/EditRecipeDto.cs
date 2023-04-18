using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Application.RecipeUtils
{
    public class EditRecipeDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageName { get; set; }
    }
}
