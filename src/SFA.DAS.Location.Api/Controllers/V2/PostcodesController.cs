using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes.V2;
using SFA.DAS.Location.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Api.Controllers.V2;

[ApiVersion("2.0")]
[ApiController]
[Route("/api/[controller]/")]
public class PostcodesController(ILogger<PostcodesController> logger,
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(LookupPostcodeV2Response), StatusCodes.Status200OK)]
    public async Task<IResult> LookupPostcode(
        [FromQuery, Required] string postcode,
        [FromServices] IPostcodeApiV2Service postcodeService,
        CancellationToken cancellationToken)
    {
        var results = await postcodeService.LookupPostcodeAsync(postcode, cancellationToken);
        return results is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(LookupPostcodeV2Response.From(results));
    }

    [HttpPost]
    [Route("bulk")]
    public async Task<IActionResult> BulkPostcode(List<string> postcodes)
    {
        try
        {
            var queryResult = await mediator.Send(new GetBulkPostcodesQueryV2(postcodes));
            var response = new GetLocationsListResponse
            {
                Locations = queryResult.Postcodes.Where(c => c != null).Select(c => (GetLocationsListItem)c).ToList()
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to get bulk postcode data from v2");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}