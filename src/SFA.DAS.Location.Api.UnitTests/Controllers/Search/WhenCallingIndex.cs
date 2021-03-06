using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Api.Controllers;
using SFA.DAS.Location.Application.Search.Queries.SearchLocations;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.Search
{
    public class WhenCallingIndex
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Locations_List_From_Mediator(
            string query,
            int results,
            GetLocationsQueryResult queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] SearchController controller)
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
            model.Locations.Should().BeEquivalentTo(queryResult.SuggestedLocations, options=>options.ExcludingMissingMembers());
        }

        [Test, MoqAutoData]
        public async Task Then_Gets_Locations_List_From_Mediator_Defaulting_To_Twenty_Items(
            string query,
            GetLocationsQueryResult queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] SearchController controller)
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
            model.Locations.Should().BeEquivalentTo(queryResult.SuggestedLocations, options=>options.ExcludingMissingMembers());
        }

        [Test, MoqAutoData]
        public async Task Then_No_Results_From_Mediator_Returns_Empty_Ok_Response(
            string query,
            GetLocationsQueryResult queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] SearchController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetLocationsQuery>(request => 
                        request.Query == query && 
                        request.ResultCount.Equals(20)), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetLocationsQueryResult{SuggestedLocations = null});
            
            var controllerResult = await controller.Index(query) as ObjectResult;

            var model = controllerResult.Value as GetLocationsListResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Locations.Should().BeEmpty();
        }

        [Test, MoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            string query,
            int results,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] SearchController controller)
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
