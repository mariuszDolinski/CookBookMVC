using CookBook.Application.Commons;
using MediatR;

namespace CookBook.Application.UnitUtils.Commands.EditUnit
{
    public class EditUnitCommand : UnitDto, IRequest<string>, IEditItemCommand
    {
        public string OldName { get; set; } = string.Empty;
    }
}
