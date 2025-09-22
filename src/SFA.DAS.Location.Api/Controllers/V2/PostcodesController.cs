using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Api.Controllers.V2;

[ApiVersion("2.0")]
[ApiController]
[Route("/api/[controller]/")]
public class PostcodesController : ControllerBase
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
}