using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IIngridientCategoryRepository
    {
        Task<IngridientCategory> GetById(int id);
    }
}
