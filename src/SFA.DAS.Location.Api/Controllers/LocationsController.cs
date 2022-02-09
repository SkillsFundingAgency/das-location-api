using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Location.Queries.GetByLocationAuthorityName;
using SFA.DAS.Location.Application.Location.Queries.GetLocalAuthorities;
using SFA.DAS.Location.Application.Location.Queries.GetRegions;

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
                    AuthorityName = authorityName
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

        [HttpGet]
        [Route("regions")]
        public async Task<IActionResult> Regions()
        {
            try
            {
                var result = await _mediator.Send(new GetRegionsQuery());
                var response = (GetRegionsListResponse) result.Regions;
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to get regions.");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("localAuthorities")]
        public async Task<IActionResult> LocalAuthorities()
        {
            try
            {
                var result = await _mediator.Send(new GetLocalAuthoritiesQuery());
                var response = (GetRegionsListResponse)result.LocalAuthorities;
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to get regions.");
                return BadRequest();
            }
        }
    }
}