using MediatR;

namespace SFA.DAS.Location.Application.Search.Queries.SearchLocalAuthority
{
    public class GetLocalAuthoritySearchQuery : IRequest<GetLocalAuthoritySearchQueryResult>
    {
        public string Query { get; set; }
        public int ResultCount { get; set; }
    }
}