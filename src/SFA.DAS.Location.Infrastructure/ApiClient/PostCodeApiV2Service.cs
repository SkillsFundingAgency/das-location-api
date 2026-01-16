using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Infrastructure.ApiClient;

public class PostCodeApiV2Service(IOsPlacesApiService osPlacesApiService): IPostcodeApiV2Service
{
    public async Task<PostcodeDataV2> LookupPostcodeAsync(string query, CancellationToken cancellationToken)
    {
        var response = await osPlacesApiService.FindFromDpaOsPlaces(query, 1, 1.0, cancellationToken);
        var address = response.FirstOrDefault();
        if (address is null)
        {
            return null;
        }

        return PostcodeDataV2.From(address);
    }
}