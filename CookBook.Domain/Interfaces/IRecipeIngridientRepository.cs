namespace CookBook.Domain.Interfaces
{
    public interface IRecipeIngridientRepository
    {
        Task DeleteRecipeIngridientById(int id);
    }
}
