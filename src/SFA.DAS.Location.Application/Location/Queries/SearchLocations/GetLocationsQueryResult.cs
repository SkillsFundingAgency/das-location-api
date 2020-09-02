using SFA.DAS.Location.Domain.Models;
using System.Collections.Generic;

namespace SFA.DAS.Location.Application.Location.Queries.SearchLocations
{
    public class GetLocationsQueryResult
    {
        public IEnumerable<SuggestedLocation> SuggestedLocations { get; set; }
    }
}