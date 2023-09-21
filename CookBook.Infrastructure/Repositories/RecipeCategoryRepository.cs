using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using CookBook.Infrastructure.CommonServices;
using CookBook.Infrastructure.Persistence;
using CookBook.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class RecipeCategoryRepository : IRecipeCategoryRepository
    {
        private readonly CookBookDbContext _dbContext;
        private readonly ICommonService<RecipeCategory> _commonService;

        public RecipeCategoryRepository(CookBookDbContext dbContext, IRepositoryUtils utils,
            ICommonService<RecipeCategory> commonService)
        {
            _dbContext = dbContext;
            _commonService = commonService;
        }

        public async Task<PaginatedResult<RecipeCategory>> GetAll(
            string searchPhrase, string sortOrder, int pageNumber, int pageSize)
                => await _commonService.GetAllItems(searchPhrase, sortOrder, pageNumber, pageSize);


        public async Task<RecipeCategory> GetById(int id)
        => await _dbContext.RecipeCategories.FirstAsync(r => r.CategoryId == id);

        public async Task<RecipeCategory?> GetByName(string name)
            => await _dbContext.RecipeCategories.FirstOrDefaultAsync(r => r.Name == name);

        public async Task Create(RecipeCategory category)
        {
            _dbContext.Add(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
