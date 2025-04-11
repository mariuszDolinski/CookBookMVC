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
        private readonly IIngridientCategoryRepository _ingCategoryRepository;

        public CreateIngridientCommandHandler(IUserContext userContext, IMapper mapper,
            IIngridientRepository ingridientRepository, IIngridientCategoryRepository categoryRepository)
        {
            _userContext = userContext;
            _mapper = mapper;
            _ingridientRepository = ingridientRepository;
            _ingCategoryRepository = categoryRepository;
        }

        public async Task Handle(CreateIngridientCommand request, CancellationToken cancellationToken)
        {
            var ingridient = _mapper.Map<Ingridient>(request);
            var ing = await _ingCategoryRepository.GetByName(request.AddInfo ?? "");
            ingridient.SetEncodedName();
            ingridient.CreatedById = _userContext.GetCurrentUser()!.Id;
            ingridient.CreatedTime = DateTime.Now;
            ingridient.CategoryId = (ing == null) ? 4 : ing.CategoryId;
            await _ingridientRepository.Create(ingridient);
        }
    }
}
