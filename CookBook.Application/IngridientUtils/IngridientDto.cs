using CookBook.Application.Commons;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Application.IngridientUtils
{
    public class IngridientDto : IItemListDto
    {
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedTime { get; set; }
        public string? AddInfo { get; set; }
        public bool IsEditable { get; set; }
        public string? EncodedName { get; private set; }
    }
}
