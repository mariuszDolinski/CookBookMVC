﻿using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<IEnumerable<Recipe>> GetAllRecipes(string? searchPhrase)
        {
            var recipes = await _dbContext.Recipes.ToListAsync();
            if(searchPhrase == null || searchPhrase == "")
            {   
                return recipes;
            }
            else
            {
                return recipes.Where(r => r.Name.ToLower().Contains(searchPhrase.ToLower())).ToList();
            }
        }

        public async Task<Recipe> GetRecipeById(int id)
            => await _dbContext.Recipes
            .Include(r => r.RecipeIngridients)
            .FirstAsync(r => r.Id == id);

        public async Task SaveChangesToDb()
            => await _dbContext.SaveChangesAsync();

        public async Task DeleteById(int id)
        {
            var recipe = await _dbContext.Recipes
                .Include(r => r.RecipeIngridients)
                .FirstAsync(r => r.Id == id);
            _dbContext.Recipes.Remove(recipe);
            await _dbContext.SaveChangesAsync();
        }
    }
}
