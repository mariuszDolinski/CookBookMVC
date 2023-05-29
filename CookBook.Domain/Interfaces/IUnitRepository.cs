using CookBook.Domain.Entities;
using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface IUnitRepository
    {
        Task<PaginatedResult<Unit>> GetAllUnits(string searchPhrase, string sortOrder, int pageNumber, int pageSize);
        Task CreateUnit(Unit unit);
        Task<Unit?> GetByName(string name);
        Task<Unit?> GetById(int id);
    }
}
