using AutoMapper;
using CookBook.Application.RecipeUtils;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.ApplicationUser.Queries.GetAllUserRecipes
{
    public class GetAllUserRecipesQueryHandler : IRequestHandler<GetAllUserRecipesQuery, IEnumerable<PreviewRecipeDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetAllUserRecipesQueryHandler(IUserRepository userRepository, IMapper mapper, 
            IUserContext userContext) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<IEnumerable<PreviewRecipeDto>> Handle(GetAllUserRecipesQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUser()!.Id;

            var userRecipes = await _userRepository.GetAllUserRecipes(userId);
            return _mapper.Map<IEnumerable<PreviewRecipeDto>>(userRecipes);
        }
    }
}
