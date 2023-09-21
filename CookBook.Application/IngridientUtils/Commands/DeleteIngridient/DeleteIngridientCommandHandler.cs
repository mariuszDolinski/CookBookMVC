using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.IngridientUtils.Commands.DeleteIngridient
{
    public class DeleteIngridientCommandHandler : IRequestHandler<DeleteIngridientCommand, string>
    {
        private readonly IUserContext _userContext;
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IRecipeIngridientRepository _riRepository;

        public DeleteIngridientCommandHandler(IUserContext userContext,
            IIngridientRepository ingridientRepository, IRecipeIngridientRepository riRepository)
        {
            _userContext = userContext;
            _ingridientRepository = ingridientRepository;
            _riRepository = riRepository;
        }

        public async Task<string> Handle(DeleteIngridientCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            if (user == null || (!user.IsInRole("Admin") && !user.IsInRole("Manager")))
            {
                return "Brak uprawnień do usunięcia składnika";
            }

            if(string.IsNullOrEmpty(request.Name))
            {
                return "Wybierz składnik do usunięcia";
            }

            var ingridient = await _ingridientRepository.GetByName(request.Name);
            if(ingridient == null)
            {
                return "Składnik, który próbujesz usunąć, nie istnieje";
            }

            var recipeIng = await _riRepository.GetRecipeIngridientByIngId(ingridient.Id);
            if(recipeIng != null)
            {
                return "Nie można usunąć składnika, gdyż jest już częścią jakiegoś przepisu";
            }

            await _ingridientRepository.DeleteById(ingridient.Id);
            return "";

        }
    }
}
