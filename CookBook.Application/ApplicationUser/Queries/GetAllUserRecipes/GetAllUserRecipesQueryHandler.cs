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
            string userName;
            if (string.IsNullOrEmpty(request.UserName))
            {
                var currentUser = _userContext.GetCurrentUser();
                if (currentUser == null)
                {
                    return new List<PreviewRecipeDto>();
                }
                else
                {
                    userName = currentUser.UserName;
                }
            }
            else
            {
                userName = request.UserName;
            }

            var userRecipes = await _userRepository.GetAllUserRecipes(userName, request.IsCurrentUser);
            return _mapper.Map<IEnumerable<PreviewRecipeDto>>(userRecipes);
        }
    }
}
