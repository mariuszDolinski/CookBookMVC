using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using CookBook.Infrastructure.CommonServices;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class IngridientCategoryRepository : IIngridientCategoryRepository
    {
        private readonly CookBookDbContext _dbContext;
        private readonly ICommonService<IngridientCategory> _commonService;

        public IngridientCategoryRepository(CookBookDbContext dbContext, ICommonService<IngridientCategory> commonService)
        {
            _dbContext = dbContext;
            _commonService = commonService;
        }

        public async Task<PaginatedResult<IngridientCategory>> GetAll(
        string searchPhrase, string sortOrder, int pageNumber, int pageSize)
                => await _commonService.GetAllItems(searchPhrase, sortOrder, pageNumber, pageSize);
        public async Task<IngridientCategory> GetById(int id)
            => await _dbContext.IngridientCategories.FirstAsync(ic => ic.CategoryId == id);

        public async Task<IngridientCategory?> GetByName(string name)
            => await _dbContext.IngridientCategories.FirstOrDefaultAsync(ic => ic.Name == name);
    }
}
