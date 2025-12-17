using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes.V1;

public class GetBulkPostcodesQueryV1Handler(IPostcodeApiService postcodeApiService) : IRequestHandler<GetBulkPostcodesQueryV1, GetBulkPostcodesQueryV1Result>
{
    public async Task<GetBulkPostcodesQueryV1Result> Handle(GetBulkPostcodesQueryV1 request, CancellationToken cancellationToken)
    {
        var data = await postcodeApiService.GetBulkPostCodeData(new GetBulkPostcodeRequest{Postcodes =  request.Postcodes});

        return new GetBulkPostcodesQueryV1Result
        {
            PostCodes = data
        };
    }
}