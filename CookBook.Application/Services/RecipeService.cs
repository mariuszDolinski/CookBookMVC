using AutoMapper;
using CookBook.Application.DtoModels;
using CookBook.Application.Services.Interfaces;
using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;

namespace CookBook.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFileService _fileService;

        public RecipeService(IMapper mapper, IRecipeRepository recipeRepository, IFileService fileService)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _fileService = fileService;
        }
        public async Task CreateRecipe(RecipeDto dto)
        {
            if(dto.ImageFile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(dto.ImageFile.FileName);
                string extension = Path.GetExtension(dto.ImageFile.FileName);
                dto.ImageName = filename + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;
            }
            else
            {
                dto.ImageName= null;
            }
            try
            {
                await _fileService.UploadImageFile(dto);
            }
            catch(Exception)
            {
                return;
            }          
            var recipe = _mapper.Map<Recipe>(dto);
            await _recipeRepository.CreateRecipe(recipe);
        }

        public async Task<IEnumerable<PreviewRecipeDto>> GetAllRecipes()
        {
            var recipes = await _recipeRepository.GetAllRecipes();
            return _mapper.Map<IEnumerable<PreviewRecipeDto>>(recipes);
        }
    }
}
