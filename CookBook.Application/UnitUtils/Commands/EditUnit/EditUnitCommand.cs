using MediatR;

namespace CookBook.Application.UnitUtils.Commands.EditUnit
{
    public class EditUnitCommand : UnitDto, IRequest<int>
    {
        public string OldName { get; set; } = string.Empty;
    }
}
