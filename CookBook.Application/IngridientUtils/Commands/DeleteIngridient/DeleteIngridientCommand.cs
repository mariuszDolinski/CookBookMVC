using MediatR;

namespace CookBook.Application.IngridientUtils.Commands.DeleteIngridient
{
    public class DeleteIngridientCommand : IngridientDto, IRequest<string>
    {
    }
}
