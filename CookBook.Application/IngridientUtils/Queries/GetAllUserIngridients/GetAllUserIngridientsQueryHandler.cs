using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetAllUserIngridients
{
    internal class GetAllUserIngridientsQueryHandler : IRequestHandler<GetAllUserIngridientsQuery, IEnumerable<IngridientDto>>
    {
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IMapper _mapper;

        public GetAllUserIngridientsQueryHandler(IIngridientRepository ingridientRepository, IMapper mapper)
        {
            _ingridientRepository = ingridientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngridientDto>> Handle(GetAllUserIngridientsQuery request, CancellationToken cancellationToken)
        {
            var ingridients = await _ingridientRepository.GetAllUserIngridients(request.UserName);

            return _mapper.Map<IEnumerable<IngridientDto>>(ingridients).ToList();
        }
    }
}
