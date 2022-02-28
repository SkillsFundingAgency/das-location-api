using MediatR;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.Search.Queries.SearchLocalAuthority
{
    public class GetLocalAuthoritySearchQueryHandler : IRequestHandler<GetLocalAuthoritySearchQuery, GetLocalAuthoritySearchQueryResult>
    {
        private readonly ILocationService _locationService;

        public GetLocalAuthoritySearchQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<GetLocalAuthoritySearchQueryResult> Handle(GetLocalAuthoritySearchQuery request, CancellationToken cancellationToken)
        {
            var result = await _locationService.GetLocationsByLocalAuthoritySearch(request.Query, request.ResultCount);

            return new GetLocalAuthoritySearchQueryResult
            {
                SuggestedLocations = result.Select(x => (SuggestedLocation)x).ToList()
            };
        }
    }
}
