using MediatR;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetByFullPostcode
{
    public class GetPostcodeQuery : IRequest<GetPostcodeQueryResult>
    {
        public string Postcode { get; set; }
    }
}
