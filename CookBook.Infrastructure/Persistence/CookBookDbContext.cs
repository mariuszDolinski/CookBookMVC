using CookBook.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Persistence
{
    public class CookBookDbContext : IdentityDbContext
    {
        public CookBookDbContext(DbContextOptions<CookBookDbContext> options) : base(options) { }
        
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngridient> RecipeIngridients { get;set; }
        public DbSet<Ingridient> Ingridients { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Servings)
                .HasMaxLength(100);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Source)
                .HasMaxLength(200);

            modelBuilder.Entity<Ingridient>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Unit>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<RecipeIngridient>()
                .Property(a => a.Amount)
                .IsRequired()
                .HasPrecision(5, 2);

            modelBuilder.Entity<RecipeIngridient>()
                .Property(a => a.Description)
                .IsRequired();

            modelBuilder.Entity<RecipeCategory>()
                .Property(c => c.CategoryName) 
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<RecipeCategory>()
                .Property(c => c.CreatedTime)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
