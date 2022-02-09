using System.Collections.Generic;

namespace SFA.DAS.Location.Api.ApiResponses
{
    public class GetRegionsListResponse
    {
        public List<string> Regions { get; set; }
        public static implicit operator GetRegionsListResponse(List<string> source) =>
            new GetRegionsListResponse { Regions = new List<string>(source) };
    }
}