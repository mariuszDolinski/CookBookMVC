using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Domain.Entities
{
    public class RecipeCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
