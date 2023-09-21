using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeCategoryById
{
    public class GetRecipeCategoryByIdQueryHandler : IRequestHandler<GetRecipeCategoryByIdQuery, RecipeCategoryDto>
    {
        private readonly IRecipeCategoryRepository _recipeCategoryRepository;
        private readonly IMapper _mapper;

        public GetRecipeCategoryByIdQueryHandler(IRecipeCategoryRepository recipeCategoryRepository, IMapper mapper)
        {
            _recipeCategoryRepository = recipeCategoryRepository;
            _mapper = mapper;
        }
        public async Task<RecipeCategoryDto> Handle(GetRecipeCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _recipeCategoryRepository.GetById(request.Id);
            return _mapper.Map<RecipeCategoryDto>(category);
        }
    }
}
