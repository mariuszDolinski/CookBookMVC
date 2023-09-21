using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CookBook.Infrastructure.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly CookBookDbContext _dbContext;
        private readonly ICommonService<Unit> _commonService;

        public UnitRepository(CookBookDbContext dbContext, ICommonService<Unit> commonService)
        {
            _dbContext = dbContext;
            _commonService = commonService;
        }

        public async Task<PaginatedResult<Unit>> GetAll(string searchPhrase, 
            string sortOrder, int pageNumber, int pageSize)
                => await _commonService.GetAllItems(searchPhrase, sortOrder, pageNumber, pageSize);

        public async Task Create(Unit unit)
        {
            unit.Name = unit.Name.ToLower();
            _dbContext.Add(unit);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Unit?> GetByName(string name)
            => await _dbContext.Units.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower());

        public async Task<Unit?> GetById(int id)
            => await _dbContext.Units.FirstOrDefaultAsync(u => u.Id == id);

        public async Task DeleteById(int id)
        {
            var ing = _dbContext.Units.First(ing => ing.Id == id);
            _dbContext.Remove(ing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesToDb()
            => await _dbContext.SaveChangesAsync();
    }
}
