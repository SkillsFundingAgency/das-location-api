using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.Location.Queries.GetRegions
{
    public class GetRegionsQueryHandler : IRequestHandler<GetRegionsQuery, GetRegionsQueryResult>
    {
        private readonly ILocationService _service;

        public GetRegionsQueryHandler(ILocationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<GetRegionsQueryResult> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
        {
            var regions = await _service.GetRegions();
            return new GetRegionsQueryResult { Regions = regions };
        }
    }
}