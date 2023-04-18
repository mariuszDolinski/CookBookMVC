using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetAllIngridients
{
    public class GetAllIngridientsQueryHandler : IRequestHandler<GetAllIngridientsQuery, IEnumerable<IngridientDto>>
    {
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IMapper _mapper;

        public GetAllIngridientsQueryHandler(IIngridientRepository ingridientRepository, IMapper mapper) 
        {
            _ingridientRepository = ingridientRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<IngridientDto>> Handle(GetAllIngridientsQuery request, CancellationToken cancellationToken)
        {
            var ingridients = await _ingridientRepository.GetAllIngridients();
            return _mapper.Map<IEnumerable<IngridientDto>>(ingridients);
        }
    }
}
