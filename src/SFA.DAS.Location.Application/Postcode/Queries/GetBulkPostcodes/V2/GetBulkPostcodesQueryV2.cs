using MediatR;
using System.Collections.Generic;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes.V2;

public sealed record GetBulkPostcodesQueryV2(List<string> Postcodes) : IRequest<GetBulkPostcodesQueryV2Result>;