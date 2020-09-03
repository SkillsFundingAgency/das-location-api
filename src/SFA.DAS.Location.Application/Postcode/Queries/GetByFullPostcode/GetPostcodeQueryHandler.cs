using MediatR;
using SFA.DAS.Location.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetByFullPostcode
{
    public class GetPostcodeQueryHandler : IRequestHandler<GetPostcodeQuery, GetPostcodeQueryResult>
    {
        private readonly IPostcodeService _service;

        public GetPostcodeQueryHandler(IPostcodeService service)
        {
            _service = service;
        }
        public async Task<GetPostcodeQueryResult> Handle(GetPostcodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetPostcodeByFullPostcode(request.Postcode);

            return new GetPostcodeQueryResult
            {
                Location = result
            };
        }
    }
}
