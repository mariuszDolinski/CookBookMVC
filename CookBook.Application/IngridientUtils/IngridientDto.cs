﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Application.IngridientUtils
{
    public class IngridientDto
    {
        public string Name { get; set; } = default!;
        public string EncodedName { get; private set; } = default!;
        public string? CreatedBy { get; set; }
        public string? CreatedTime { get; set; }
    }
}
