using CookBook.Application.ApplicationUser;
using CookBook.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Application.RecipeUtils.Commands.DeleteRecipeIngridient
{
    public class DeleteRecipeIngridientCommandHandler : IRequestHandler<DeleteRecipeIngridientCommand>
    {
        private readonly IRecipeIngridientRepository _riRepository;
        private readonly IUserContext _userContext;

        public DeleteRecipeIngridientCommandHandler(IRecipeIngridientRepository riRepository, 
            IUserContext userContext)
        {
            _riRepository = riRepository;
            _userContext = userContext;
        }
        public async Task Handle(DeleteRecipeIngridientCommand request, CancellationToken cancellationToken)
        {
           await _riRepository.DeleteRecipeIngridientById(request.recipeIngridientId);
        }
    }
}
