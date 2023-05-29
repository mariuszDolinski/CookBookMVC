using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetAllUnits(string searchPhrase, string sortOrder, int pageNumber, int pageSize);
        Task CreateUnit(Unit unit);
        Task<Unit?> GetByName(string name);
        Task<Unit?> GetById(int id);
        Task<int> GetCount(string searchPhrase);
    }
}
