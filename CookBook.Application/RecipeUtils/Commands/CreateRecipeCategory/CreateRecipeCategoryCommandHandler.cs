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
        private readonly IUserContext _userContext;

        public CreateRecipeCategoryCommandHandler(IMapper mapper, IRecipeRepository recipeRepository, 
            IUserContext userContext)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _userContext = userContext;
        }
        public async Task Handle(CreateRecipeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<RecipeCategory>(request);
            category.CreatedById = _userContext.GetCurrentUser()!.Id;
            category.CreatedTime = DateTime.Now;
            await _recipeRepository.CreateCategory(category);
        }
    }
}
