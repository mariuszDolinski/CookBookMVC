using CookBook.Application.CommonDtos;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Application.IngridientUtils
{
    public class IngridientDto : ItemListDto
    {
        public string? EncodedName { get; private set; }
    }
}
