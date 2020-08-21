using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Location.Queries;

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
        public async Task<IActionResult> Index([FromQuery]string query, [FromQuery]int results)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetLocationsQuery
                {
                    Query = query,
                    ResultCount = results
                });
                
                var response = new GetLocationsListResponse
                {
                    Locations = queryResult.Locations.Select(c=>(GetLocationsListItem)c).ToList()
                };
                
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e,$"Unable to get location data for {query} - number of results {results}");
                return BadRequest();
            }
        }
    }
}