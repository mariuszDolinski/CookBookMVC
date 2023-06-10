using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.IngridientUtils.Commands.EditIngridient
{
    public class EditIngridientCommandHandler : IRequestHandler<EditIngridientCommand, int>
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
        /// <summary>
        /// return 1 if success, 0 if user is not authrized, -1 if new name exists, -2 in other cases
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Handle(EditIngridientCommand request, CancellationToken cancellationToken)
        {
            var ingridient = await _ingridientRepository.GetByName(request.OldName);

            if(request.OldName.ToLower() != request.Name!.ToLower())
            {
                var ing = await _ingridientRepository.GetByName(request.Name);
                if(ing != null)
                {
                    return -1;
                }
            }

            if(ingridient == null)
            {
                return -2;
            }

            var user = _userContext.GetCurrentUser();
            if (user == null || !(user.IsInRole("Admin") || user.IsInRole("Manager")))
            {
                return 0;
            }

            ingridient.Name = request.Name!;
            ingridient.SetEncodedName();
            ingridient.LastEdit = DateTime.Now;

            await _ingridientRepository.SaveChangesToDb();
            return 1;

        }
    }
}
