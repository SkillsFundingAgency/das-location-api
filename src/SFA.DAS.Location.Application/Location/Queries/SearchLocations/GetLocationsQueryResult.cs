using System.Collections.Generic;

namespace SFA.DAS.Location.Application.Location.Queries.SearchLocations
{
    public class GetLocationsQueryResult
    {
        public IEnumerable<Domain.Entities.Location> Locations { get ; set ; }
    }
}