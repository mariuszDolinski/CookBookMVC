using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Application.RecipeUtils.Commands.DeleteRecipeCategory
{
    internal class DeleteRecipeCategoryCommandHandler : IRequestHandler<DeleteRecipeCategoryCommand, string>
    {
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IRecipeCategoryRepository _recipeCategoryRepository;
        private readonly IRecipeRepository _recipeRepository;

        public DeleteRecipeCategoryCommandHandler(IUserContext userContext, IMapper mapper,
            IRecipeCategoryRepository recipeCategoryRepository, IRecipeRepository recipeRepository)
        {
            _userContext = userContext;
            _mapper = mapper;
            _recipeCategoryRepository = recipeCategoryRepository;
            _recipeRepository = recipeRepository;
        }

        public async Task<string> Handle(DeleteRecipeCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            if (user == null || (!user.IsInRole("Admin") && !user.IsInRole("Manager")))
            {
                return "Brak uprawnień do usunięcia kategorii";
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return "Wybierz kategorię do usunięcia";
            }

            var category = await _recipeCategoryRepository.GetByName(request.Name);
            if (category == null)
            {
                return "Kategoria, którą próbujesz usunąć, nie istnieje";
            }

            var recipe = await _recipeRepository.GetRecipeByCategoryId(category.CategoryId);
            if (recipe != null)
            {
                return "Nie można usunąć kategorii, gdyż zawiera już przepisy";
            }

            await _recipeCategoryRepository.DeleteById(category.CategoryId);
            return "";
        }
    }
}
