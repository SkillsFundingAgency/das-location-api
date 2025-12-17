using MediatR;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes.V2;
public class GetBulkPostcodesQueryV2Handler(IAddressesService addressesService) : IRequestHandler<GetBulkPostcodesQueryV2, GetBulkPostcodesQueryV2Result>
{
    public async Task<GetBulkPostcodesQueryV2Result> Handle(GetBulkPostcodesQueryV2 request, CancellationToken cancellationToken)
    {
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = 10,          
            CancellationToken = cancellationToken
        };

        var results = new ConcurrentBag<PostcodeData>();

        await Parallel.ForEachAsync(request.Postcodes, options, async (postcode, ct) =>
        {
            var response = await addressesService
                .FindFromDpaOsPlaces(postcode, 1.0, ct);

            var suggestedAddresses = response.ToList();
            if (!suggestedAddresses.Any())
                return;

            var first = suggestedAddresses.First();

            results.Add(new PostcodeData
            {
                Postcode = first.Postcode,
                Lat = Convert.ToDouble(first.Latitude),
                Long = Convert.ToDouble(first.Longitude),
                Country = first.Country
            });
        });

        var list = results.ToList();

        return new GetBulkPostcodesQueryV2Result(list);
    }
}
