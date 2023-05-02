using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Application.Services.Interfaces;
using CookBook.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Application.RecipeUtils.Commands.EditImage
{
    public class EditImageCommandHandler : IRequestHandler<EditImageCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFileService _fileService;
        private readonly IUserContext _userContext;

        public EditImageCommandHandler(IMapper mapper, IRecipeRepository recipeRepository,
            IFileService fileService, IUserContext userContext)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _fileService = fileService;
            _userContext = userContext;
        }

        public async Task Handle(EditImageCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetRecipeById(request.Id);
            string? imageName = null;

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (recipe.AuthorId == user.Id || user.IsInRole("Manager"));

            if (!isEditable)
            {
                throw new InvalidOperationException("Access denied");
            }

            if (recipe.ImageName != null)
            {
                await _fileService.DeleteImageFile(recipe.ImageName);
            }
            try
            {
                if(request.ImageFile != null) 
                {
                    imageName = await _fileService.UploadImageFile(request.ImageFile);
                }               
            }
            catch (Exception)
            {
                throw new Exception("Couldn't upload the image");
            }

            recipe.ImageName = imageName;
            await _recipeRepository.SaveChangesToDb();
        }
    }
}
