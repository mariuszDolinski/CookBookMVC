using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories
{
    public class GetAllRecipeCategoriesQueryHandler :
        IRequestHandler<GetAllRecipeCategoriesQuery, IEnumerable<RecipeCategoryDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllRecipeCategoriesQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;        
        }
        public async Task<IEnumerable<RecipeCategoryDto>> Handle(GetAllRecipeCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _userRepository.GetAllRecipeCategories();
            return _mapper.Map<IEnumerable<RecipeCategoryDto>>(categories);
        }
    }
}
