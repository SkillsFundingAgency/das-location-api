using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Location.Application.Search.Queries.SearchLocations;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Application.Search.Queries.SearchLocations
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
            var postcodeRegexFull = @"^[A-Za-z]{1,2}[0-9]{1}([0-9]|[A-Za-z]){0,1}";
            var postcodeRegexOneLetterOneNumber = @"^[A-Za-z]{1}[0-9]{1}";

            if (request.Query.Length < 3 && Regex.IsMatch(request.Query, postcodeRegexOneLetterOneNumber))
            {
                var postcodes = await _postcodeService.GetPostcodeByOutcodeQuery(request.Query, request.ResultCount);

                return new GetLocationsQueryResult
                {
                    SuggestedLocations = postcodes.ToList()
                };
            }
            else if (request.Query.Length >= 3 && Regex.IsMatch(request.Query, postcodeRegexFull))
            {
                var postcodes = await _postcodeService.GetPostcodeByOutcodeQuery(request.Query, request.ResultCount);

                return new GetLocationsQueryResult
                {
                    SuggestedLocations = postcodes.ToList()
                };
            }
            else if (request.Query.Length >= 3)
            {
                var result = await _locationService.GetLocationsByQuery(request.Query, request.ResultCount);

                return new GetLocationsQueryResult
                {
                    SuggestedLocations = result.Select(c => (SuggestedLocation)c).ToList()
                };
            }

            return new GetLocationsQueryResult
            {
                SuggestedLocations = null
            };
        }
    }
}