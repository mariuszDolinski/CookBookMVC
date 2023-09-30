using CookBook.Domain.Entities;
using CookBook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Seeders
{
    public class CookBookSeeder
    {
        private readonly CookBookDbContext _dbContext;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public CookBookSeeder(CookBookDbContext dbContext) 
        {
            _dbContext = dbContext;
            //_roleManager = roleManager;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if(pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }

                if(!_dbContext.Units.Any())
                {
                    var units = GetUnits();
                    _dbContext.Units.AddRange(units);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Ingridients.Any())
                {
                    var ingridients = GetIngridients();
                    _dbContext.Ingridients.AddRange(ingridients);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.RecipeCategories.Any())
                {
                    var categories = GetRecipeCategories();
                    _dbContext.RecipeCategories.AddRange(categories);
                    await _dbContext.SaveChangesAsync();
                }
                
                //CreateDefaultRoles();

            }
        }

        private IEnumerable<Unit> GetUnits()
        {
            var units = new List<Unit>();
            foreach (var unit in Enum.GetNames(typeof(UnitsEnum)))
            {
                units.Add(new Unit() { Name = unit });
                units.Last().SetEncodedName();
                units.Last().CreatedTime = DateTime.UtcNow;
            }

            return units;
        }
        private IEnumerable<Ingridient> GetIngridients()
        {
            var ingridients = new List<Ingridient>();
            string replaceString;
            foreach (var ing in Enum.GetNames(typeof(IngridientsEnum)))
            {
                replaceString = ing.Replace('_', ' ');
                ingridients.Add(new Ingridient() { Name = replaceString });
                ingridients.Last().SetEncodedName();
                ingridients.Last().CreatedTime = DateTime.UtcNow;
            }

            return ingridients;
        }
        private IEnumerable<RecipeCategory> GetRecipeCategories()
        {
            var categories = new List<RecipeCategory>
            {
                new RecipeCategory() { Name = "Zupy" },
                new RecipeCategory() { Name = "Inne" }
            };
            //set default CategoryId value on 2 "Inne"
            foreach (var r in _dbContext.Recipes)
            {
                r.CategoryId = 2;
            }

            return categories;
        }

        //private async void CreateDefaultRoles()
        //{
           // string[] roleNames = { "Admin", "Manager", "User" };
            //foreach (var role in roleNames)
            //{
                //var roleExists = await _roleManager.RoleExistsAsync(role);
                //if (!roleExists)
                //{
                  //  await _roleManager.CreateAsync(new IdentityRole(role));
                //}
            //}
        //}
    }
}
