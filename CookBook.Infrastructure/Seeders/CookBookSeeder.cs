using CookBook.Domain.Entities;
using CookBook.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Infrastructure.Seeders
{
    public class CookBookSeeder
    {
        private readonly CookBookDbContext _dbContext;

        public CookBookSeeder(CookBookDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
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
            }
        }

        private IEnumerable<Unit> GetUnits()
        {
            var units = new List<Unit>();
            foreach (var unit in Enum.GetNames(typeof(UnitsEnum)))
            {
                units.Add(new Unit() { Name = unit });
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
            }

            return ingridients;
        }
    }
}
