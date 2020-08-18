using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Location.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class LocationController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Ok("ok");
        }
    }
}