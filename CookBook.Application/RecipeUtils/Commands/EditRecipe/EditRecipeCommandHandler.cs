using AutoMapper;
using CookBook.Application.Services.Interfaces;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.EditRecipe
{
    public class EditRecipeCommandHandler : IRequestHandler<EditRecipeCommand>
    {
        private readonly IRecipeRepository _recipeRepository;

        public EditRecipeCommandHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task Handle(EditRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetRecipeById(request.Id);

            recipe.Name = request.Name!;
            recipe.Description = request.Description;
            recipe.OnlyForAdults = request.OnlyForAdults;
            await _recipeRepository.SaveChangesToDb();
        }
    }
}
