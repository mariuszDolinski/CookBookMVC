using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Application.DtoModels
{
    public class PreviewRecipeDto
    {
        public string Name { get; set; } = default!;
        public bool OnlyForAdults { get; set; } = false;
        public string? ImageName { get; set; }
    }
}
