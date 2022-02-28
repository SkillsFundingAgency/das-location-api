using SFA.DAS.Location.Domain.Models;
using System.Collections.Generic;

namespace SFA.DAS.Location.Application.Search.Queries.SearchLocalAuthority
{
    public class GetLocalAuthoritySearchQueryResult
    {
        public List<SuggestedLocation> SuggestedLocations { get; set; }
    }
}