using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class IngridientRepository : IIngridientRepository
    {
        private readonly CookBookDbContext _dbContext;

        public IngridientRepository(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Ingridient>> GetAllIngridients()
            => await _dbContext.Ingridients.ToListAsync();

        public async Task CreateIngridient(Ingridient ingridient)
        {
            _dbContext.Add(ingridient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ingridient?> GetByName(string name)
            => await _dbContext.Ingridients.FirstOrDefaultAsync(ing => ing.Name.ToLower() == name.ToLower());
    }
}
