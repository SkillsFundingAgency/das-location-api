using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Location.Application.Postcode.Queries.GetByFullPostcode
{
    public class GetPostcodeQuery : IRequest<GetPostcodeQueryResult>
    {
        public string Postcode { get; set; }
    }
}
