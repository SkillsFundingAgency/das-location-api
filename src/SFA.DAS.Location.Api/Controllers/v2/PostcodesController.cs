using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes;
using SFA.DAS.Location.Application.Postcode.Queries.GetByFullPostcode;
using SFA.DAS.Location.Application.Postcode.Queries.GetByOutcode;

namespace SFA.DAS.Location.Api.Controllers.v2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class PostcodesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PostcodesController> _logger;

        public PostcodesController(IMediator mediator, ILogger<PostcodesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index([FromQuery] string postcode)
        {
            var queryResult = await _mediator.Send(new GetPostcodeQuery { Postcode = postcode }); ;
            if (queryResult.Postcode == null)
            {
                return Ok(new GetLocationsListItem());
            }

            var response = (GetLocationsListItem)queryResult.Postcode;
            return Ok(response);
        }
    }
}
