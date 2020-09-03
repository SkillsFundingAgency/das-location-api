using MediatR;

namespace SFA.DAS.Location.Application.Location.Queries.GetByLocationAuthorityName
{
    public class GetLocationQuery : IRequest<GetLocationQueryResult>
    {
        public string LocationName { get ; set ; }
        public string AuthorityName { get ; set ; }
    }
}