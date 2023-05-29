using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IIngridientRepository
    {
        Task<IEnumerable<Ingridient>> GetAllIngridients(string searchPhrase, string sortOrder, int pageNumber, int pageSize);
        Task CreateIngridient(Ingridient ingridient);
        Task<Ingridient?> GetByName(string name);
        Task<Ingridient?> GetById(int id);
        Task<int> GetCount(string searchPhrase);
    }
}
