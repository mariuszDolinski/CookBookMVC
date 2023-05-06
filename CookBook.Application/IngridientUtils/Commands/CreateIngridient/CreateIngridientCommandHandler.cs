using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using MediatR;
namespace CookBook.Application.IngridientUtils.Commands.CreateIngridient
{
    public class CreateIngridientCommandHandler : IRequestHandler<CreateIngridientCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IIngridientRepository _ingridientRepository;

        public CreateIngridientCommandHandler(IUserContext userContext, IMapper mapper,
            IIngridientRepository ingridientRepository)
        {
            _userContext = userContext;
            _mapper = mapper;
            _ingridientRepository = ingridientRepository;
        }

        public async Task Handle(CreateIngridientCommand request, CancellationToken cancellationToken)
        {
            var ingridient = _mapper.Map<Ingridient>(request);
            ingridient.SetEncodedName();
            ingridient.CreatedById = _userContext.GetCurrentUser()!.Id;
            ingridient.CreatedTime = DateTime.Now;
            await _ingridientRepository.CreateIngridient(ingridient);
        }
    }
}
