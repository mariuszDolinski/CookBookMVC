using MediatR;

namespace CookBook.Application.UnitUtils.Queries.GetAllUnits
{
    public class GetAllUnitsQuery : IRequest<IEnumerable<UnitDto>>
    {
    }
}
