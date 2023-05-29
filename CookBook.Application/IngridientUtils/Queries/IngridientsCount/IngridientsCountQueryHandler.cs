using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.IngridientsCount
{
    public class IngridientsCountQueryHandler : IRequestHandler<IngridientsCountQuery, int>
    {
        private readonly IIngridientRepository _ingridientRepository;

        public IngridientsCountQueryHandler(IIngridientRepository ingridientRepository)
        {
            _ingridientRepository = ingridientRepository;
        }

        public async Task<int> Handle(IngridientsCountQuery request, CancellationToken cancellationToken)
        {
            return await _ingridientRepository.GetCount(request.SearchPhrase);
        }
    }
}
