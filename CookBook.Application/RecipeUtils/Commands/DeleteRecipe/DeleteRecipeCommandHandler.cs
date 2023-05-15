using CookBook.Application.ApplicationUser;
using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.DeleteRecipe
{
    public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserContext _userContext;

        public DeleteRecipeCommandHandler(IRecipeRepository recipeRepository, IUserContext userContext)
        {
            _recipeRepository = recipeRepository;
            _userContext = userContext;
        }
        public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var recipe = await _recipeRepository.GetRecipeById(request.Id);
            var hasAccess = user != null && user.Id == recipe.AuthorId;
            if (!hasAccess)
            {
                return;
            }

            await _recipeRepository.DeleteById(request.Id);
        }
    }
}
