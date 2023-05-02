using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Application.Services.Interfaces;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.EditRecipe
{
    public class EditRecipeCommandHandler : IRequestHandler<EditRecipeCommand>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserContext _userContext;

        public EditRecipeCommandHandler(IRecipeRepository recipeRepository, IUserContext userContext
            )
        {
            _recipeRepository = recipeRepository;
            _userContext = userContext;
        }
        public async Task Handle(EditRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetRecipeById(request.Id);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (recipe.AuthorId == user.Id || user.IsInRole("Manager"));

            if(!isEditable)
            {
                throw new InvalidOperationException("Access denied");
            }

            recipe.Name = request.Name!;
            recipe.Description = request.Description;
            recipe.OnlyForAdults = request.OnlyForAdults;
            await _recipeRepository.SaveChangesToDb();
        }
    }
}
