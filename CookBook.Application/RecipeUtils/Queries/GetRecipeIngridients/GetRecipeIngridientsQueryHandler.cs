using AutoMapper;
using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeIngridients
{
    public class GetRecipeIngridientsQueryHandler
        : IRequestHandler<GetRecipeIngridientsQuery, IEnumerable<RecipeIngridientDto>>
    {
        private readonly IRecipeIngridientRepository _recipeIngridientRepository;
        private readonly IMapper _mapper;
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IUnitRepository _unitRepository;

        public GetRecipeIngridientsQueryHandler(IRecipeIngridientRepository recipeIngridientRepository, IMapper mapper,
            IIngridientRepository ingridientRepository, IUnitRepository unitRepository)
        {
            _recipeIngridientRepository = recipeIngridientRepository;
            _mapper = mapper;
            _ingridientRepository = ingridientRepository;
            _unitRepository = unitRepository;
        }
        public async Task<IEnumerable<RecipeIngridientDto>> Handle(GetRecipeIngridientsQuery request, CancellationToken cancellationToken)
        {
            var recipeIngridients = await _recipeIngridientRepository.GetAllRecipeIngridients(request.RecipeId);

            List<Ingridient?> ingridients = new List<Ingridient?>();
            List<Domain.Entities.Unit?> units = new List<Domain.Entities.Unit?>();
            foreach( var item in recipeIngridients )
            {
                ingridients.Add(await _ingridientRepository.GetById(item.IngridientId));
                units.Add(await _unitRepository.GetById(item.UnitId));
            }

            var dto = _mapper.Map<IEnumerable<RecipeIngridientDto>>(recipeIngridients);
            int k = 0;
            foreach(var item in dto)
            {
                item.Ingridient = ingridients[k]!.Name;
                item.Unit = units[k]!.Name;
                k++;
            }
            return dto;
        }
    }
}
