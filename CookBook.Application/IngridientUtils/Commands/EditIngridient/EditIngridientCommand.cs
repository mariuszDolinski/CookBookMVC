using MediatR;

namespace CookBook.Application.IngridientUtils.Commands.EditIngridient
{
    public class EditIngridientCommand : IngridientDto, IRequest<int>
    {
        public string OldName { get; set; } = string.Empty;
    }
}
