using AutoMapper.Internal.Mappers;
using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetAllIngridients
{
    public class GetAllIngridientsQuery : IRequest<IEnumerable<IngridientDto>>
    {
    }
}
