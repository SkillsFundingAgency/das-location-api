using MediatR;
using SFA.DAS.Location.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.Addresses.Queries
{
    public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, GetAddressesQueryResult>
    {
        private readonly IAddressesService _service;

        public GetAddressesQueryHandler(IAddressesService service)
        {
            _service = service;
        }

        public async Task<GetAddressesQueryResult> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            var result =
                await _service.FindFromLpiDataset(request.Query, request.MinMatch);

            return new GetAddressesQueryResult
            {
                Addresses = result
            };
        }
    }
}