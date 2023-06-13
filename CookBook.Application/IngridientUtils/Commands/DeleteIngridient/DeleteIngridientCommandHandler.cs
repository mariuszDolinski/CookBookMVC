using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.IngridientUtils.Commands.DeleteIngridient
{
    public class DeleteIngridientCommandHandler : IRequestHandler<DeleteIngridientCommand, int>
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
        /// <summary>
        /// return 1 if succeded, 0 if ingridient is a part of at least one recipe ingridient, <0 other error
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Handle(DeleteIngridientCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            if (user == null || (!user.IsInRole("Admin") && !user.IsInRole("Manager")))
            {
                return -1;
            }

            if(string.IsNullOrEmpty(request.Name))
            {
                return -2;
            }

            var ingridient = await _ingridientRepository.GetByName(request.Name);
            if(ingridient == null)
            {
                return -3;
            }

            var recipeIng = await _riRepository.GetRecipeIngridientByIngId(ingridient.Id);
            if(recipeIng != null)
            {
                return 0;
            }

            await _ingridientRepository.DeleteById(ingridient.Id);
            return 1;

        }
    }
}
