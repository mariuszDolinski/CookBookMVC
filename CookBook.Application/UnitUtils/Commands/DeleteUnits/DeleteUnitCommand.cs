using MediatR;

namespace CookBook.Application.UnitUtils.Commands.DeleteUnits
{
    public class DeleteUnitCommand : UnitDto, IRequest<string>
    {
    }
}
