﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Application.DtoModels
{
    public class RecipeDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool OnlyForAdults { get; set; } = false;
        public IFormFile? ImageFile { get; set; }
        public string? ImageName { get; set; }
    }
}
