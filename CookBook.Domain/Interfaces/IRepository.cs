namespace CookBook.Domain.Interfaces
{
    public interface IRepository
    {
        Task SaveChangesToDb();
    }
}
