using System.Collections.Generic;

namespace SFA.DAS.Location.Application.Location.Queries
{
    public class GetLocationsResponse
    {
        public IEnumerable<Domain.Entities.Location> Locations { get ; set ; }
    }
}