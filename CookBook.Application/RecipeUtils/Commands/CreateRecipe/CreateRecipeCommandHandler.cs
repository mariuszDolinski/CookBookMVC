using AutoMapper;
using CookBook.Application.ApplicationUser;
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
        private readonly IUserContext _userContext;

        public CreateRecipeCommandHandler(IMapper mapper, IRecipeRepository recipeRepository, 
            IFileService fileService, IUserContext userContext)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _fileService = fileService;
            _userContext = userContext;
        }
        public async Task Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.ImageFile != null)
                {
                    request.ImageName = await _fileService.UploadImageFile(request.ImageFile);
                }               
            }
            catch (Exception)
            {
                throw new Exception("Couldn't upload the image");
            }
            var recipe = _mapper.Map<Recipe>(request);
            recipe.AuthorId = _userContext.GetCurrentUser().Id;
            await _recipeRepository.CreateRecipe(recipe);
        }
    }
}
