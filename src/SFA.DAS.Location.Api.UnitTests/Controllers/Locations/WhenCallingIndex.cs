using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Api.Controllers;
using SFA.DAS.Location.Application.Location.Queries;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.Locations
{
    public class WhenCallingIndex
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Locations_List_From_Mediator(
            string query,
            int results,
            GetLocationsQueryResult queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] LocationsController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetLocationsQuery>(request => 
                        request.Query == query && 
                        request.ResultCount.Equals(results)), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.Index(query, results) as ObjectResult;

            var model = controllerResult.Value as GetLocationsListResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Locations.Should().BeEquivalentTo(queryResult.Locations, options=>options.ExcludingMissingMembers());
        }
        
        
        [Test, MoqAutoData]
        public async Task Then_Gets_Locations_List_From_Mediator_Defaulting_To_Twenty_Items(
            string query,
            GetLocationsQueryResult queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] LocationsController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetLocationsQuery>(request => 
                        request.Query == query && 
                        request.ResultCount.Equals(20)), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.Index(query) as ObjectResult;

            var model = controllerResult.Value as GetLocationsListResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Locations.Should().BeEquivalentTo(queryResult.Locations, options=>options.ExcludingMissingMembers());
        }

        [Test, MoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            string query,
            int results,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] LocationsController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetLocationsQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.Index(query, results) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}