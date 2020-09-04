using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Search.Queries.SearchLocations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SearchController> _logger;

        public SearchController(IMediator mediator, ILogger<SearchController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index([FromQuery] string query, [FromQuery] int results = 20)
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
                    Locations = queryResult.SuggestedLocations.Select(c => (GetLocationsListItem)c).ToList()
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to get location data for {query} - number of results {results}");
                return BadRequest();
            }
        }
    }
}
