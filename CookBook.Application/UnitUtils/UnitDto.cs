using CookBook.Application.CommonDtos;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Application.UnitUtils
{
    public class UnitDto : ItemListDto
    {
        public string? EncodedName { get; private set; }
    }
}
