using CookBook.Domain.Entities;
using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface IIngridientCategoryRepository
    {
        Task<PaginatedResult<IngridientCategory>> GetAll(string searchPhrase, string sortOrder, int pageNumber, int pageSize);
        Task<IngridientCategory> GetById(int id);
    }
}
