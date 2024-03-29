﻿using AutoMapper;
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
                throw new Exception("Nie można załadować zdjęcia");
            }

            var recipe = _mapper.Map<Recipe>(request);
            //var categoryId = await _recipeRepository.GetCategoryIdByName(request.CategoryName!);
            //if(categoryId == 0)
            //{
            //    return;
            //}
            recipe.AuthorId = _userContext.GetCurrentUser()!.Id;
            recipe.CreatedTime = DateTime.Now;
            recipe.CategoryId = request.CategoryId;
            await _recipeRepository.CreateRecipe(recipe);
        }
    }
}
