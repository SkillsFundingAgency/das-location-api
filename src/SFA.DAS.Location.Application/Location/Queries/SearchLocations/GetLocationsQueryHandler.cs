using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Application.Location.Queries.SearchLocations
{
    public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, GetLocationsQueryResult>
    {
        private readonly ILocationService _locationService;
        private readonly IPostcodeService _postcodeService;

        public GetLocationsQueryHandler(ILocationService locationService, IPostcodeService postcodeService)
        {
            _locationService = locationService;
            _postcodeService = postcodeService;
        }
        public async Task<GetLocationsQueryResult> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            var regex = @"^[A-Za-z]{1,2}[0-9]{1}([0-9]|[A-Za-z]){0,1}$";
            
            if (Regex.IsMatch(request.Query, regex))
            {
                var postcodes = await _postcodeService.GetPostcodeByOutcodeQuery(request.Query, request.ResultCount);

                return new GetLocationsQueryResult
                {
                    SuggestedLocations = postcodes.ToList()
                };
            }
            else
            {
                var result = await _locationService.GetLocationsByQuery(request.Query, request.ResultCount);

                return new GetLocationsQueryResult
                {
                    SuggestedLocations = result.Select(c => (SuggestedLocation)c).ToList()
                };
            }            
        }
    }
}