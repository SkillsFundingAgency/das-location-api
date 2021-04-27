using MediatR;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetByOutcode
{
    public class GetOutcodeQuery : IRequest<GetOutcodeQueryResult>
    {
        public string Outcode { get; set; }

    }
}
