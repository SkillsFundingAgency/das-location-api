﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Addresses.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;

namespace SFA.DAS.Location.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(IMediator mediator, ILogger<AddressesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index(string query, double minMatch)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetAddressesQuery
                {
                    Query = query,
                    MinMatch = minMatch
                });

                var response = new GetAddressesListResponse
                {
                    Addresses = queryResult.Addresses.Select(c => (GetAddressesListItem)c).ToList()
                };

                return Ok(response);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex, $"Unable to get address data for query:{query}/{minMatch}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to get address data for query:{query}/{minMatch}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
