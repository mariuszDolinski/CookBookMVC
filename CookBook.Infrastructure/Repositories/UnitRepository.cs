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
    }
}
