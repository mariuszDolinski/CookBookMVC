using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IIngridientRepository
    {
        Task<IEnumerable<Ingridient>> GetAllIngridients();
        Task CreateIngridient(Ingridient ingridient);
        Task<Ingridient?> GetByName(string name);
        Task<Ingridient?> GetById(int id);
    }
}
