using CookBook.Domain.Entities;
using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface IIngridientRepository : IRepository
    {
        Task<PaginatedResult<Ingridient>> GetAll(string searchPhrase, string sortOrder, int pageNumber, int pageSize);
        Task<IEnumerable<Ingridient>> GetAllUserIngridients(string userName);
        Task Create(Ingridient ingridient);
        Task<Ingridient?> GetByName(string name);
        Task<Ingridient?> GetById(int id);
        Task DeleteById(int id);
    }
}
