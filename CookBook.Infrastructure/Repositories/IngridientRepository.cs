﻿using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Domain.Pagination;
using CookBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CookBook.Infrastructure.Repositories
{
    public class IngridientRepository : IIngridientRepository
    {
        private readonly CookBookDbContext _dbContext;
        private readonly ICommonService<Ingridient> _commonService;
        private readonly IIngridientCategoryRepository _categoryRepository;

        public IngridientRepository(CookBookDbContext dbContext, ICommonService<Ingridient> commonService,
            IIngridientCategoryRepository categoryRepository)
        {
            _dbContext = dbContext;
            _commonService = commonService;
            _categoryRepository = categoryRepository;
        }

        public async Task<PaginatedResult<Ingridient>> GetAll(string searchPhrase,
            string sortOrder, int pageNumber, int pageSize)
        {
            var results = await _commonService.GetAllItems(searchPhrase, sortOrder, pageNumber, pageSize);

            results.Items.ForEach(ing => {
                ing.Category = _categoryRepository.GetById(ing.CategoryId).Result;
            });
            return results;
        }

        public async Task<IEnumerable<Ingridient>> GetAllUserIngridients(string userName)
            => await _commonService.GetAllUserItems(userName);

        public async Task Create(Ingridient ingridient)
        {
            ingridient.Name = ingridient.Name.ToLower();
            _dbContext.Add(ingridient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ingridient?> GetByName(string name)
            => await _dbContext.Ingridients.FirstOrDefaultAsync(ing => ing.Name.ToLower() == name.ToLower());

        public async Task<Ingridient?> GetById(int id)
            => await _dbContext.Ingridients.FirstOrDefaultAsync(ing => ing.Id == id);

        public async Task DeleteById(int id)
        {
            var ing = _dbContext.Ingridients.First(ing => ing.Id == id);
            _dbContext.Remove(ing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesToDb()
            => await _dbContext.SaveChangesAsync();
    }
}
