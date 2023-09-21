using CookBook.Application.Commons;
using MediatR;

namespace CookBook.Application.IngridientUtils.Commands.EditIngridient
{
    public class EditIngridientCommand : IngridientDto, IRequest<string>, IEditItemCommand
    {
        public string OldName { get; set; } = string.Empty;
    }
}
