﻿using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CookBookDbContext _dbContext;

        public RecipeRepository(CookBookDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task CreateRecipe(Recipe recipe)
        {
            _dbContext.Add(recipe);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
            => await _dbContext.Recipes.ToListAsync();
    }
}
