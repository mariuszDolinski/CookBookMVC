using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.EditRecipeCategory
{
    public class EditRecipeCategoryCommandHandler : IRequestHandler<EditRecipeCategoryCommand, string>
    {
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IRecipeCategoryRepository _recipeCategoryRepository;

        public EditRecipeCategoryCommandHandler(IUserContext userContext, IMapper mapper,
            IRecipeCategoryRepository recipeCategoryRepository)
        {
            _userContext = userContext;
            _mapper = mapper;
            _recipeCategoryRepository = recipeCategoryRepository;
        }

        public async Task<string> Handle(EditRecipeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _recipeCategoryRepository.GetByName(request.OldName);

            if (request.OldName.ToLower() != request.Name!.ToLower())
            {
                var ing = await _recipeCategoryRepository.GetByName(request.Name);
                if (ing != null)
                {
                    return "Składnik o podanej nazwie już istnieje";
                }
            }

            if (category == null)
            {
                return "Edycja składnika nie powiodła się";
            }

            var user = _userContext.GetCurrentUser();
            if (user == null || !(user.IsInRole("Admin") || user.IsInRole("Manager")))
            {
                return "Brak uprawnień do edycji składnika";
            }

            category.Name = request.Name!;
            category.LastEdit = DateTime.Now;

            await _recipeCategoryRepository.SaveChangesToDb();
            return "";
        }
    }
}
