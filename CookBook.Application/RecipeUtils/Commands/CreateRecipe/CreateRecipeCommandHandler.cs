using AutoMapper;
using CookBook.Application.Services.Interfaces;
using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipe
{
    public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFileService _fileService;

        public CreateRecipeCommandHandler(IMapper mapper, IRecipeRepository recipeRepository, 
            IFileService fileService)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _fileService = fileService;
        }
        public async Task Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.ImageFile != null)
                {
                    await _fileService.UploadImageFile(request.ImageFile);
                }               
            }
            catch (Exception)
            {
                throw new Exception("Couldn't upload the image");
            }
            var recipe = _mapper.Map<Recipe>(request);
            await _recipeRepository.CreateRecipe(recipe);
        }
    }
}
