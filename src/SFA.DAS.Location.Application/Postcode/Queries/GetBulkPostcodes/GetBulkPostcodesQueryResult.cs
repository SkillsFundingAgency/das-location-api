using System.Collections.Generic;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes;

public class GetBulkPostcodesQueryResult
{
    public List<PostcodeData> PostCodes { get; set; }
}