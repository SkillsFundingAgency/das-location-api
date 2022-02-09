using System.Collections.Generic;

namespace SFA.DAS.Location.Api.ApiResponses
{
    public class GetLocalAuthoritiesListResponse
    {
        public List<string> LocalAuthorities { get; set; }

        public static implicit operator GetLocalAuthoritiesListResponse(List<string> source) =>
            new GetLocalAuthoritiesListResponse {LocalAuthorities = new List<string>(source)};
    }
}