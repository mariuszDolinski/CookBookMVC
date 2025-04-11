using CookBook.Application.Commons;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Application.UnitUtils
{
    public class UnitDto : IItemListDto
    {
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedTime { get; set; }
        public string? AddInfo { get; set; } = "";
        public bool IsEditable { get; set; }
        public string? EncodedName { get; private set; }
    }
}
