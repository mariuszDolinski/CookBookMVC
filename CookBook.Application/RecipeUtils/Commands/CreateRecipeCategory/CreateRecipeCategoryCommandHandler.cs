using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Application.Services;
using CookBook.Application.Services.Interfaces;
using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipeCategory
{
    public class CreateRecipeCategoryCommandHandler : IRequestHandler<CreateRecipeCategoryCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;
        public CreateRecipeCategoryCommandHandler(IMapper mapper, IRecipeRepository recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task Handle(CreateRecipeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<RecipeCategory>(request);
            await _recipeRepository.CreateCategory(category);
        }
    }
}
