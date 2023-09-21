using CookBook.Domain.Entities;
using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface IUnitRepository : IRepository
    {
        Task<PaginatedResult<Unit>> GetAll(string searchPhrase, string sortOrder, int pageNumber, int pageSize);
        Task Create(Unit unit);
        Task<Unit?> GetByName(string name);
        Task<Unit?> GetById(int id);
        Task DeleteById(int id);
        Task SaveChangesToDb();

    }
}
