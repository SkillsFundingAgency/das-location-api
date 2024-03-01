using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Location.Infrastructure.ApiClient;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes;

public class GetBulkPostcodesQueryHandler(IPostcodeApiService postcodeApiService) : IRequestHandler<GetBulkPostcodesQuery, GetBulkPostcodesQueryResult>
{
    public async Task<GetBulkPostcodesQueryResult> Handle(GetBulkPostcodesQuery request, CancellationToken cancellationToken)
    {
        var data = await postcodeApiService.GetBulkPostCodeData(request.Postcodes);

        return new GetBulkPostcodesQueryResult
        {
            PostCodes = data
        };
    }
}