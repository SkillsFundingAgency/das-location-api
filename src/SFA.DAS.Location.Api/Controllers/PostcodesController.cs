using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Postcode.Queries.GetByFullPostcode;
using System;
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
                    return NotFound();
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
    }
}
