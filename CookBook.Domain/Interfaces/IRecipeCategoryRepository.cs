using CookBook.Domain.Entities;
using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface IRecipeCategoryRepository : IRepository
    {
        Task<PaginatedResult<RecipeCategory>> GetAll(string searchPhrase, string sortOrder, int pageNumber, int pageSize);
        Task<RecipeCategory> GetById(int id);
        Task<RecipeCategory?> GetByName(string name);
        Task Create(RecipeCategory category);
    }
}
