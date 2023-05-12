using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.EditRecipeIngridient
{
    public class EditRecipeIngridientCommandHandler : IRequestHandler<EditRecipeIngridientCommand, bool>
    {
        private readonly IRecipeIngridientRepository _riRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserContext _userContext;

        public EditRecipeIngridientCommandHandler(IRecipeIngridientRepository riRepository, IRecipeRepository recipeRepository,
            IIngridientRepository ingridientRepository, IUnitRepository unitRepository, IUserContext userContext)
        {
            _riRepository = riRepository;
            _recipeRepository = recipeRepository;
            _ingridientRepository = ingridientRepository;
            _unitRepository = unitRepository;
            _userContext = userContext;
        }
        public async Task<bool> Handle(EditRecipeIngridientCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetRecipeById(request.RecipeId);
            var user = _userContext.GetCurrentUser();

            var hasAccess = user != null && user.Id == recipe.AuthorId;
            if (!hasAccess)
            {
                return false;
            }

            var recipeIngridient = await _riRepository.GetRecipeIngridientById(request.Id);
            var ingridient = await _ingridientRepository.GetByName(request.Ingridient!);
            var unit = await _unitRepository.GetByName(request.Unit!);

            recipeIngridient.Amount = request.Amount!;
            recipeIngridient.Description = request.Description!;
            recipeIngridient.IngridientId = ingridient!.Id;
            recipeIngridient.UnitId = unit!.Id;

            await _recipeRepository.SaveChangesToDb();

            return true;
        }
    }
}
