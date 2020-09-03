using MediatR;

namespace SFA.DAS.Location.Application.Search.Queries.SearchLocations
{
    public class GetLocationsQuery : IRequest<GetLocationsQueryResult>
    {
        public string Query { get ; set ; }
        public int ResultCount { get ; set ; }
    }
}