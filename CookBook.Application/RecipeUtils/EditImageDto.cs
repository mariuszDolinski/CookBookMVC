using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Application.RecipeUtils
{
    public class EditImageDto
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
