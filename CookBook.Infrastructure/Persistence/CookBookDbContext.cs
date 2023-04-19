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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

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
        }
    }
}
