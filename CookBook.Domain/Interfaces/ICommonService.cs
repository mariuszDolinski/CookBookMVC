using CookBook.Domain.Pagination;

namespace CookBook.Domain.Interfaces
{
    public interface ICommonService<T> where T : class, IEntity, new()
    {
        Task<PaginatedResult<T>> GetAllItems
            (string searchPhrase, string sortOrder, int pageNumber, int pageSize);

        Task<IEnumerable<T>> GetAllUserItems(string userName);
    }
}
