using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.Location.Queries.GetLocalAuthorities
{
    public class GetLocalAuthoritiesQueryHandler : IRequestHandler<GetLocalAuthoritiesQuery, GetLocalAuthoritiesQueryResult>
    {
        private readonly ILocationService _service;

        public GetLocalAuthoritiesQueryHandler(ILocationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<GetLocalAuthoritiesQueryResult> Handle(GetLocalAuthoritiesQuery request, CancellationToken cancellationToken)
        {
            var localAuthorities = await _service.GetLocalAuthorities();
            return new GetLocalAuthoritiesQueryResult { LocalAuthorities = localAuthorities };
        }
    }
}