using System.Collections.Generic;
using MediatR;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes.V1;

public class GetBulkPostcodesQueryV1 : IRequest<GetBulkPostcodesQueryV1Result>
{
    public List<string> Postcodes { get; set; }
}