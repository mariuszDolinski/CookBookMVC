using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeIngridients
{
    public class GetRecipeIngridientsQueryHandler
        : IRequestHandler<GetRecipeIngridientsQuery, RecipeIngridientDto>
    {
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IUnitRepository _unitRepository;

        public GetRecipeIngridientsQueryHandler(IIngridientRepository ingridientRepository, 
            IUnitRepository unitRepository)
        {
            _ingridientRepository = ingridientRepository;
            _unitRepository = unitRepository;
        }
        public async Task<RecipeIngridientDto> Handle(GetRecipeIngridientsQuery request, CancellationToken cancellationToken)
        {
            RecipeIngridientDto dto = new RecipeIngridientDto();

            var ingridients = await _ingridientRepository.GetAllIngridients();
            var units = await _unitRepository.GetAllUnits();

            return dto;
        }
    }
}
