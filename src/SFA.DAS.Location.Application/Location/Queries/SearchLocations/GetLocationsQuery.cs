using MediatR;

namespace SFA.DAS.Location.Application.Location.Queries.SearchLocations
{
    public class GetLocationsQuery : IRequest<GetLocationsQueryResult>
    {
        public string Query { get ; set ; }
        public int ResultCount { get ; set ; }
    }
}