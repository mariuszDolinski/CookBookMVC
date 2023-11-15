using MediatR;

namespace CookBook.Application.UnitUtils.Queries.GetAllUserUnits
{
    public class GetAllUserUnitsQuery : IRequest<IEnumerable<UnitDto>>
    {
        public string UserName { get; set; }

        public GetAllUserUnitsQuery() 
        {
            UserName = string.Empty;
        }

        public GetAllUserUnitsQuery(string userName)
        {
            UserName = userName;
        }
    }
}
