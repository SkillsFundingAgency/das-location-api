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
        // Limit the degree of parallelism to avoid exceeding OS Places API rate limits
        // https://docs.os.uk/os-apis/core-concepts/rate-limiting-policy
        var results = new ConcurrentBag<PostcodeData>();

        await Parallel.ForEachAsync(request.Postcodes, new ParallelOptions
        {
            MaxDegreeOfParallelism = 10, 
            CancellationToken = cancellationToken
        }, async (postcode, token) =>
        {
            var response = await addressesService
                .FindFromDpaOsPlaces(postcode, 1.0, token);

            var suggestedAddresses = response.ToList();

            // No results for this postcode - skip it
            if (suggestedAddresses.Count == 0)
                return;

            // Take the first result as the best match for the postcode
            var first = suggestedAddresses.First();
            results.Add(new PostcodeData
            {
                Postcode = first.Postcode,
                Lat = Convert.ToDouble(first.Latitude),
                Long = Convert.ToDouble(first.Longitude),
                Country = first.Country
            });
        });

        return new GetBulkPostcodesQueryV2Result(results.ToList());
    }
}
