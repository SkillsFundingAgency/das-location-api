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
using SFA.DAS.Location.Application.Location.Queries.GetByLocationAuthorityName;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.Locations
{
    public class WhenCallingIndex
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Location_From_Mediator(
            string authorityName,
            string locationName,
            GetLocationQueryResult queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] LocationsController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetLocationQuery>(request => 
                        request.LocationName == locationName && 
                        request.AuthorityName == authorityName), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.Index(locationName, authorityName) as ObjectResult;

            var model = controllerResult.Value as GetLocationsListItem;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Should().BeEquivalentTo(queryResult.Location, options=>options.ExcludingMissingMembers());
        }
        
        [Test, MoqAutoData]
        public async Task And_Returns_Null_Returns_Not_Found(
            string authorityName,
            string locationName,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] LocationsController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetLocationQuery>(),
                    It.IsAny<CancellationToken>())).ReturnsAsync(new GetLocationQueryResult
                {
                    Location = null
                });
            
            var controllerResult = await controller.Index(locationName, authorityName) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        
        [Test, MoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            string authorityName,
            string locationName,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] LocationsController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetLocationQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.Index(locationName, authorityName) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}