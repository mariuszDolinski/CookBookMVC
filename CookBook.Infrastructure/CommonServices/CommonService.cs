using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using CookBook.Infrastructure.Extensions;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Reflection;

namespace CookBook.Infrastructure.CommonServices
{
    public class CommonService<T> : ICommonService<T> where T : class, IEntity, new()
    {
        private readonly CookBookDbContext _dbContext;

        public CommonService(CookBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<T>> GetAllItems(string searchPhrase, string sortOrder, int pageNumber, int pageSize)
        {
            var entitySet = _dbContext.Set<T>();
            var baseQuery = entitySet.Include(ing => ing.CreatedBy)
                .Where(ing => string.IsNullOrEmpty(searchPhrase) ? true : ing.Name.Contains(searchPhrase));

            var item = new T();//potrzebny obiekt typu T do wyłuskania nazw kolumn
            var columnSelector = new Dictionary<string, Expression<Func<T, object>>>()
            {
                {nameof(item.Name), ing => ing.Name },
                {nameof(item.CreatedTime), ing => ing.CreatedTime },
                {nameof(item.CreatedBy), ing => (ing.CreatedBy != null) ? ing.CreatedBy.UserName! : "-"}
            };

            string selectedColumn;
            if (sortOrder.Contains("date"))
                selectedColumn = nameof(item.CreatedTime);
            else if (sortOrder.Contains("author"))
                selectedColumn = nameof(item.CreatedBy);
            else
                selectedColumn = nameof(item.Name);

            if (sortOrder.Contains("desc"))
                baseQuery = baseQuery
                    .OrderByDescending(columnSelector[selectedColumn]);
            else
                baseQuery = baseQuery
                    .OrderBy(columnSelector[selectedColumn]);

            List<T> items;
            if (pageNumber == 0)
            {
                items = await baseQuery.ToListAsync();
            }
            else
            {
                items = await baseQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return new PaginatedResult<T>(items, baseQuery.Count());
        }
    }
}
