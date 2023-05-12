using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetAllUnits();
        Task CreateUnit(Unit unit);
        Task<Unit?> GetByName(string name);
        Task<Unit?> GetById(int id);
    }
}
