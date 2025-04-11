using CookBook.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Domain.Entities
{
    public class IngridientCategory : IEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public string? CreatedById { get; set; }
        public AppUser? CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? LastEdit { get; set; } = DateTime.Now;

        public List<Ingridient> Ingridients { get; set; } = new List<Ingridient>();
    }
}
