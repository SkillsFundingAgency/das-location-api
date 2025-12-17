using System.Collections.Generic;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes.V2;
public sealed record GetBulkPostcodesQueryV2Result(List<Domain.Models.PostcodeData> Postcodes);