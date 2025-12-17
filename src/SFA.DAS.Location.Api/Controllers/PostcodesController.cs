using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes.V1;
using SFA.DAS.Location.Application.Postcode.Queries.GetByFullPostcode;
using SFA.DAS.Location.Application.Postcode.Queries.GetByOutcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Api.Controllers
{
    [ApiVersion("1.0")]
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
        public async Task<IActionResult> Index(string postcode)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetPostcodeQuery
                {
                    Postcode = postcode
                }); ;

                if (queryResult.Postcode == null)
                {
                    return Ok(new GetLocationsListItem());
                }

                var response = (GetLocationsListItem)queryResult.Postcode;

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to get location data for postcode:{postcode}");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("outcode")]
        public async Task<IActionResult> Outcode([FromQuery]string outcode)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetOutcodeQuery
                {
                    Outcode = outcode
                }); 

                if (queryResult.Outcode == null)
                {
                    return Ok(new GetLocationsListItem());
                }

                var response = (GetLocationsListItem)queryResult.Outcode;

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to get location data for postcode:{outcode}");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("bulk")]
        public async Task<IActionResult> BulkPostcode(List<string> postcodes)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetBulkPostcodesQueryV1
                {
                    Postcodes = postcodes
                });

                var response = new GetLocationsListResponse
                {
                    Locations = queryResult.PostCodes.Where(c => c != null).Select(c => (GetLocationsListItem)c).ToList()
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to get bulk postcode data from v1");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
