using CookBook.Domain.Entities;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Utilities
{
    public class RepositoryUtils : IRepositoryUtils
    {
        /// <summary>
        /// Check if recipe meets condition in Advanced Search.
        /// Used in GetAllRecipe method in RecipeRepository
        /// </summary>
        /// <param name="searchList">array of searched ingridients names</param>
        /// <param name="recipeIngs">list of current recipe ingridients</param>
        /// <param name="mode">search mode</param>
        /// <param name="dbContext">context of database</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsRecipeMeetsSearchCondition
            (string[] searchList, List<RecipeIngridient> recipeIngs, int mode, CookBookDbContext dbContext)
        {
            if (recipeIngs.Count == 0) { return false; }

            switch (mode)
            {
                case 1:
                    foreach (var ing in searchList)
                    {
                        bool isOnTheList = false;
                        foreach (var ri in recipeIngs)
                        {
                            var name = dbContext.Ingridients.FirstOrDefault(i => i.Id == ri.IngridientId)!.Name;
                            if (name != null && name.Contains(ing))
                            {
                                isOnTheList = true;
                                break;
                            }
                        }
                        if (!isOnTheList)
                        {
                            return false;
                        }
                    }
                    break;
                case 2:
                    foreach (var ri in recipeIngs)
                    {
                        bool isOnTheList = false;
                        var name = dbContext.Ingridients.FirstOrDefault(i => i.Id == ri.IngridientId)!.Name;
                        foreach (var ing in searchList)
                        {
                            if (name.Contains(ing))
                            {
                                isOnTheList = true;
                                break;
                            }
                        }
                        if (!isOnTheList)
                        {
                            return false;
                        }
                    }
                    break;
                default: 
                    return false;
            }

            return true;
        }
    }
}
