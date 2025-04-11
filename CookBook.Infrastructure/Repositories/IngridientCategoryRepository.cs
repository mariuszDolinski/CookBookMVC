using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class IngridientCategoryRepository : IIngridientCategoryRepository
    {
        private readonly CookBookDbContext _dbContext;

        public IngridientCategoryRepository(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IngridientCategory> GetById(int id)
            => await _dbContext.IngridientCategories.FirstAsync(ic => ic.CategoryId == id);
    }
}
