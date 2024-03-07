using System.Collections.Generic;
using MediatR;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes;

public class GetBulkPostcodesQuery : IRequest<GetBulkPostcodesQueryResult>
{
    public List<string> Postcodes { get; set; }
}