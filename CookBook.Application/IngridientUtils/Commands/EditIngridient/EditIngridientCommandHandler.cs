using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.IngridientUtils.Commands.EditIngridient
{
    public class EditIngridientCommandHandler : IRequestHandler<EditIngridientCommand, string>
    {
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IIngridientRepository _ingridientRepository;

        public EditIngridientCommandHandler(IUserContext userContext, IMapper mapper,
            IIngridientRepository ingridientRepository)
        {
            _userContext = userContext;
            _mapper = mapper;
            _ingridientRepository = ingridientRepository;
        }

        public async Task<string> Handle(EditIngridientCommand request, CancellationToken cancellationToken)
        {
            var ingridient = await _ingridientRepository.GetByName(request.OldName);

            if(request.OldName.ToLower() != request.Name!.ToLower())
            {
                var ing = await _ingridientRepository.GetByName(request.Name);
                if(ing != null)
                {
                    return "Składnik o podanej nazwie już istnieje";
                }
            }

            if(ingridient == null)
            {
                return "Edycja składnika nie powiodła się";
            }

            var user = _userContext.GetCurrentUser();
            if (user == null || !(user.IsInRole("Admin") || user.IsInRole("Manager")))
            {
                return "Brak uprawnień do edycji składnika";
            }

            ingridient.Name = request.Name!;
            ingridient.SetEncodedName();
            ingridient.LastEdit = DateTime.Now;

            await _ingridientRepository.SaveChangesToDb();
            return "";

        }
    }
}
