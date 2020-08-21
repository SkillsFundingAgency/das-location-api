using MediatR;

namespace SFA.DAS.Location.Application.Location.Queries
{
    public class GetLocationsQuery : IRequest<GetLocationsResponse>
    {
        public string Query { get ; set ; }
        public int ResultCount { get ; set ; }
    }
}