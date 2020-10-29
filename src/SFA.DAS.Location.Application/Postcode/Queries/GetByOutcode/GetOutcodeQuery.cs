using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetByOutcode
{
    public class GetOutcodeQuery : IRequest<GetOutcodeQueryResult>
    {
        public string Outcode { get; set; }

    }
}
