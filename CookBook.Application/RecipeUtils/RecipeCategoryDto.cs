using CookBook.Application.Commons;

namespace CookBook.Application.RecipeUtils
{
    public class RecipeCategoryDto : IItemListDto
    {
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedTime { get; set; }
        public bool IsEditable { get; set; }
        public int CategoryId { get; set; }
    }
}
