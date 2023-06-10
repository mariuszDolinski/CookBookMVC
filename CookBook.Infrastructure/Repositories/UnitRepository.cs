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

        public UnitRepository(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<Unit>> GetAllUnits(string searchPhrase, string sortOrder, int pageNumber, int pageSize)
        {
            var baseQuery = _dbContext.Units
                .Include(ing => ing.CreatedBy)
                .Where(ing => string.IsNullOrEmpty(searchPhrase) ? true : ing.Name.Contains(searchPhrase));

            var columnSelector = new Dictionary<string, Expression<Func<Unit, object>>>()
            {
                {nameof(Unit.Name), ing => ing.Name },
                {nameof(Unit.CreatedTime), ing => ing.CreatedTime },
                {nameof(Unit.CreatedBy), ing => (ing.CreatedBy != null) ? ing.CreatedBy.UserName! : "-"}
            };

            string selectedColumn;
            if (sortOrder.Contains("date"))
                selectedColumn = nameof(Unit.CreatedTime);
            else if (sortOrder.Contains("author"))
                selectedColumn = nameof(Unit.CreatedBy);
            else
                selectedColumn = nameof(Unit.Name);

            if (sortOrder.Contains("desc"))
                baseQuery = baseQuery
                    .OrderByDescending(columnSelector[selectedColumn]);
            else
                baseQuery = baseQuery
                    .OrderBy(columnSelector[selectedColumn]);

            List<Unit> items;
            if (pageNumber == 0)
            {
                items = await baseQuery.ToListAsync();
            }
            else
            {
                items = await baseQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return new PaginatedResult<Unit>(items, baseQuery.Count());
        }

        public async Task CreateUnit(Unit unit)
        {
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
