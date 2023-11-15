using MediatR;

namespace CookBook.Application.IngridientUtils.Queries.GetAllUserIngridients
{
    public class GetAllUserIngridientsQuery : IRequest<IEnumerable<IngridientDto>>
    {
        public string UserName { get; set; }

        public GetAllUserIngridientsQuery() 
        {
            UserName = string.Empty;
        }

        public GetAllUserIngridientsQuery(string userName)
        {
            UserName = userName;
        }
    }
}
