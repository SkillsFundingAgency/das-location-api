using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Application.LocationImport.Handlers.ImportLocations;

namespace SFA.DAS.Location.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/ops/dataload/")]
    public class DataLoadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DataLoadController> _logger;

        public DataLoadController (
            IMediator mediator,
            ILogger<DataLoadController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Data import request received");
                await _mediator.Send(new ImportDataCommand());
                _logger.LogInformation("Data import completed successfully");
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Data import failed");
                return BadRequest();
            }
        }
    }
}