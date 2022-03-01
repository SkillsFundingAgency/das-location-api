using SFA.DAS.Location.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetByOutcode
{
    public class GetOutcodeQueryHandler : IRequestHandler<GetOutcodeQuery, GetOutcodeQueryResult>
    {
        private readonly IPostcodeService _postcodeService;
        private readonly ILocationService _locationService;

        public GetOutcodeQueryHandler(IPostcodeService postcodeService, ILocationService locationService)
        {
            _postcodeService = postcodeService;
            _locationService = locationService;
        }
        public async Task<GetOutcodeQueryResult> Handle(GetOutcodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _postcodeService.GetDistrictNameByOutcodeQuery(request.Outcode);

            if (result != null)
            {
                var locations = await _locationService.GetLocationsByLocalAuthorityDistrict(result.LocalAuthorityDistrict);
                result.Region = locations.First().Region;
            }

            return new GetOutcodeQueryResult
            {
                Outcode = result
            };
        }
    }
}
