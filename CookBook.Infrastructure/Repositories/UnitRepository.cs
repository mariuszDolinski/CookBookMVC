using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly CookBookDbContext _dbContext;

        public UnitRepository(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Unit>> GetAllUnits()
            => await _dbContext.Units.ToListAsync();
        public async Task CreateUnit(Unit unit)
        {
            _dbContext.Add(unit);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Unit?> GetByName(string name)
            => await _dbContext.Units.FirstOrDefaultAsync(unit => unit.Name.ToLower() == name.ToLower());
    }
}
