using SFA.DAS.Location.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetByOutcode
{
    public class GetOutcodeQueryHandler : IRequestHandler<GetOutcodeQuery, GetOutcodeQueryResult>
    {
        private readonly IPostcodeService _service;

        public GetOutcodeQueryHandler(IPostcodeService service)
        {
            _service = service;
        }
        public async Task<GetOutcodeQueryResult> Handle(GetOutcodeQuery request, CancellationToken cancellationToken)
        {

            var result = await _service.GetDistrictNameByOutcodeQuery(request.Outcode );

            return new GetOutcodeQueryResult
            {
                Outcode = result
            };
        }
    }
}
