using CookBook.Application.ApplicationUser;
using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipeIngridient
{
    public class CreateRecipeIngridientCommandHandler : IRequestHandler<CreateRecipeIngridientCommand, bool>
    {
        private readonly IUserContext _userContext;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IUnitRepository _unitRepository;

        public CreateRecipeIngridientCommandHandler(IUserContext userContext, IRecipeRepository recipeRepository,
            IIngridientRepository ingridientRepository, IUnitRepository unitRepository) 
        {
            _userContext = userContext;
            _recipeRepository = recipeRepository;
            _ingridientRepository = ingridientRepository;
            _unitRepository = unitRepository;
        }

        public async Task<bool> Handle(CreateRecipeIngridientCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetRecipeById(request.RecipeId);
            var user = _userContext.GetCurrentUser();

            var hasAccess = user != null && user.Id == recipe.AuthorId;
            if(!hasAccess)
            {
                return false;
            }

            var ingridient = await _ingridientRepository.GetByName(request.Ingridient!);
            var unit = await _unitRepository.GetByName(request.Unit!);

            var recipeIngridient = new RecipeIngridient()
            {
                RecipeId = recipe.Id,
                Amount = request.Amount!,
                Description = request.Description!,
                IngridientId = ingridient!.Id,
                UnitId = unit!.Id
            };

            await _recipeRepository.AddRecipeIngridient(recipeIngridient);

            return true;
        }
    }
}
