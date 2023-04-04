using AutoMapper;
using CookBook.Application.DtoModels;
using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;

namespace CookBook.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IMapper mapper, IRecipeRepository recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task CreateRecipe(RecipeDto dto)
        {
            var recipe = _mapper.Map<Recipe>(dto);
            await _recipeRepository.CreateRecipe(recipe);
        }
    }
}
