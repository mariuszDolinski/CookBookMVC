using CookBook.Domain.Entities;

namespace CookBook.Domain.Interfaces
{
    public interface IIngridientRepository
    {
        Task<IEnumerable<Ingridient>> GetAllIngridients();
    }
}
