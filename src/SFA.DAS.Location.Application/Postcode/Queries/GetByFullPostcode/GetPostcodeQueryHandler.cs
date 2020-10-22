﻿using MediatR;
using SFA.DAS.Location.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
            string FullPostcodeRegex = @"^[A-Za-z]{1,2}\d[A-Za-z\d]?\s*\d[A-Za-z]{2}$";

            if (Regex.IsMatch(FullPostcodeRegex, request.Postcode))
            {
                var result = await _service.GetPostcodeByFullPostcode(request.Postcode);

                return new GetPostcodeQueryResult
                {
                    Postcode = result
                };
            }

            var outcodeResult = await _service.GetPostcodeDataByOutcode(request.Postcode);
            return new GetPostcodeQueryResult
            {
                Postcode = outcodeResult
            };
            
        }
    }
}
