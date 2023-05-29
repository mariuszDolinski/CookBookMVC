using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CookBook.Infrastructure.Repositories
{
    public class IngridientRepository : IIngridientRepository
    {
        private readonly CookBookDbContext _dbContext;

        public IngridientRepository(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Ingridient>> GetAllIngridients(string searchPhrase, string sortOrder, int pageNumber, int pageSize)
        {
            var baseQuery = _dbContext.Ingridients
                .Include(ing => ing.CreatedBy)
                .Where(ing => string.IsNullOrEmpty(searchPhrase) ? true : ing.Name.Contains(searchPhrase));

            var columnSelector = new Dictionary<string, Expression<Func<Ingridient, object>>>()
            {
                {nameof(Ingridient.Name), ing => ing.Name },
                {nameof(Ingridient.CreatedTime), ing => ing.CreatedTime },
                {nameof(Ingridient.CreatedBy), ing => (ing.CreatedBy != null) ? ing.CreatedBy.UserName! : "-"}
            };

            string selectedColumn;
            if(sortOrder.Contains("date"))
                selectedColumn = nameof(Ingridient.CreatedTime);
            else if(sortOrder.Contains("author"))
                selectedColumn = nameof(Ingridient.CreatedBy);
            else
                selectedColumn = nameof(Ingridient.Name);

            if (sortOrder.Contains("desc"))
                baseQuery = baseQuery
                    .OrderByDescending(columnSelector[selectedColumn]);
            else
                baseQuery = baseQuery
                    .OrderBy(columnSelector[selectedColumn]);

            if(pageNumber == 0)
            {
                return await baseQuery.ToListAsync();
            }
            else
            {
                return await baseQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            
        }

        public async Task CreateIngridient(Ingridient ingridient)
        {
            _dbContext.Add(ingridient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ingridient?> GetByName(string name)
            => await _dbContext.Ingridients.FirstOrDefaultAsync(ing => ing.Name.ToLower() == name.ToLower());

        public async Task<Ingridient?> GetById(int id)
            => await _dbContext.Ingridients.FirstOrDefaultAsync(ing => ing.Id == id);

        public async Task<int> GetCount(string searchPhrase)
            => await _dbContext.Ingridients
            .Where(ing => string.IsNullOrEmpty(searchPhrase) ? true : ing.Name.Contains(searchPhrase))
            .CountAsync();
    }
}
