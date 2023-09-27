using CookBook.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Domain.Entities
{
    public class RecipeCategory : IEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public string? CreatedById { get; set; }
        public AppUser? CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? LastEdit { get; set; } = DateTime.Now;

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
