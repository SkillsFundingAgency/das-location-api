using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Location.Queries.GetByLocationAuthorityName;

namespace SFA.DAS.Location.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController (IMediator mediator, ILogger<LocationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index(string locationName, string authorityName)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetLocationQuery
                {
                    LocationName = locationName,
                    AuthorityName = authorityName != null ? authorityName : null
                });

                if (queryResult.Location == null)
                {
                    return NotFound();
                }

                var response = (GetLocationsListItem) queryResult.Location; 
                
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e,$"Unable to get location data for location:{locationName} authority:{authorityName}");
                return BadRequest();
            }
        }
    }
}