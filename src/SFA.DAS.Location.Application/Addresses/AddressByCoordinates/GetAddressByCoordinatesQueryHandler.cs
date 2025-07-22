using SFA.DAS.Location.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.Addresses.AddressByCoordinates;
public class GetAddressByCoordinatesQueryHandler(IAddressesService service) : MediatR.IRequestHandler<GetAddressByCoordinatesQuery, GetAddressByCoordinatesQueryResult>
{
    public async Task<GetAddressByCoordinatesQueryResult> Handle(GetAddressByCoordinatesQuery request, CancellationToken cancellationToken)
    {
        var response = await service.NearestFromDpaDataset($"{request.Latitude},{request.Longitude}");

        return new GetAddressByCoordinatesQueryResult(response);            
    }
}