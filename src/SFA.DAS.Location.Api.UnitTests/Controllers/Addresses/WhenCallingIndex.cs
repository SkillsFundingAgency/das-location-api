using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Api.Controllers;
using SFA.DAS.Location.Application.Addresses.Queries;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.Addresses
{
    public class WhenCallingIndex
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Locations_List_From_Mediator(
            string query,
            double minMatch,
            GetAddressesQueryResult queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AddressesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetAddressesQuery>(request => 
                        request.Query == query && 
                        request.MinMatch.Equals(minMatch)), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.Index(query, minMatch) as ObjectResult;

            var model = controllerResult.Value as GetAddressesListResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Addresses.Should().BeEquivalentTo(queryResult.Addresses, options=>options.ExcludingMissingMembers());
        }

        [Test, MoqAutoData]
        public async Task Then_No_Results_From_Mediator_Returns_Empty_Ok_Response(
            string query,
            double minMatch,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AddressesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetAddressesQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetAddressesQueryResult { Addresses = new List<SuggestedAddress>() });
            
            var controllerResult = await controller.Index(query, minMatch) as ObjectResult;

            var model = controllerResult.Value as GetAddressesListResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Addresses.Should().BeEmpty();
        }

        [Test, MoqAutoData]
        public async Task And_ArgumentOutOfRange_Exception_Then_Returns_Bad_Request(
            string query,
            double minMatch,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AddressesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetAddressesQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<ArgumentOutOfRangeException>();

            var controllerResult = await controller.Index(query, minMatch) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test, MoqAutoData]
        public async Task And_Any_Other_Exception_Then_Returns_Internal_Server_Error(
            string query,
            double minMatch,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AddressesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetAddressesQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var controllerResult = await controller.Index(query, minMatch) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
