using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.Location.Queries.GetByLocationAuthorityName
{
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, GetLocationQueryResult>
    {
        private readonly ILocationService _service;

        public GetLocationQueryHandler (ILocationService service)
        {
            _service = service;
        }
        public async Task<GetLocationQueryResult> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            var result = request.AuthorityName != null ?
                await _service.GetLocationsByLocationAuthorityName(request.LocationName, request.AuthorityName) :
                await _service.GetLocationsByLocationName(request.LocationName);

            return new GetLocationQueryResult
            {
                Location = result
            };
        }
    }
}