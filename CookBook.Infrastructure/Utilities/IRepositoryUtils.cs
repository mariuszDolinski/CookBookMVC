using CookBook.Domain.Entities;
using CookBook.Infrastructure.Persistence;

namespace CookBook.Infrastructure.Utilities
{
    public interface IRepositoryUtils
    {
        public bool IsRecipeMeetsSearchCondition
            (string[] searchList, List<RecipeIngridient> recipeIngs, int mode, CookBookDbContext dbContext);
    }
}
