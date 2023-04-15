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
            if (request.ImageFile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(request.ImageFile.FileName);
                string extension = Path.GetExtension(request.ImageFile.FileName);
                request.ImageName = filename + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;
            }
            else
            {
                request.ImageName = null;
            }
            try
            {
                await _fileService.UploadImageFile(request);
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
