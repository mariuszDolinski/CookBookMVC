using CookBook.Domain.Entities;
using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface IIngridientRepository
    {
        Task<PaginatedResult<Ingridient>> GetAllIngridients(string searchPhrase, string sortOrder, int pageNumber, int pageSize);
        Task CreateIngridient(Ingridient ingridient);
        Task<Ingridient?> GetByName(string name);
        Task<Ingridient?> GetById(int id);
        Task SaveChangesToDb();
        Task DeleteById(int id);
    }
}
