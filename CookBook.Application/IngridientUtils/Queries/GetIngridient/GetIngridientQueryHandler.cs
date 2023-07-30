using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetIngridient
{
    public class GetIngridientQueryHandler : IRequestHandler<GetIngridientQuery, int>
    {
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IMapper _mapper;

        public GetIngridientQueryHandler(IIngridientRepository ingridientRepository, IMapper mapper)
        {
            _ingridientRepository = ingridientRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// return 0 if given name is empty or null, 1 if ingridient is found, 2 if ingridient is not found
        /// </summary>
        /// <param name="request">ingridient name</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Handle(GetIngridientQuery request, CancellationToken cancellationToken)
        {
            if (request.IngName == null || request.IngName == "empty")
            {
                return 0;
            }
            var ingridient = await _ingridientRepository.GetByName(request.IngName);
            if (ingridient == null)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}
