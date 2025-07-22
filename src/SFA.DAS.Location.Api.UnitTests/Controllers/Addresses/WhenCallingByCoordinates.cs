using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Api.Controllers;
using SFA.DAS.Location.Application.Addresses.AddressByCoordinates;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.Addresses;
[TestFixture]
public class WhenCallingByCoordinates
{
    [Test, MoqAutoData]
    public async Task Then_Gets_Locations_List_From_Mediator(
            double latitude,
            double longitude,
            GetAddressByCoordinatesQueryResult queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AddressesController controller)
    {
        mockMediator
            .Setup(mediator => mediator.Send(
                It.Is<GetAddressByCoordinatesQuery>(request =>
                    request.Latitude.Equals(latitude) &&
                    request.Longitude.Equals(longitude)),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        var controllerResult = await controller.Nearest(latitude, longitude) as ObjectResult;

        var model = controllerResult.Value as SuggestedPlace;
        controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        model.Should().BeEquivalentTo(queryResult.Place, options => options.ExcludingMissingMembers());
    }

    [Test, MoqAutoData]
    public async Task Then_No_Results_From_Mediator_Returns_Empty_Ok_Response(
        double latitude,
        double longitude,
        GetAddressByCoordinatesQueryResult queryResult,
        [Frozen] Mock<IMediator> mockMediator,
        [Greedy] AddressesController controller)
    {
        mockMediator
            .Setup(mediator => mediator.Send(
                It.IsAny<GetAddressByCoordinatesQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetAddressByCoordinatesQueryResult(new SuggestedPlace()));

        var controllerResult = await controller.Nearest(latitude, longitude) as ObjectResult;

        var model = controllerResult.Value as GetAddressByCoordinatesQueryResult;
        controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Test, MoqAutoData]
    public async Task And_Any_Other_Exception_Then_Returns_Internal_Server_Error(
        double latitude,
        double longitude,
        GetAddressByCoordinatesQueryResult queryResult,
        [Frozen] Mock<IMediator> mockMediator,
        [Greedy] AddressesController controller)
    {
        mockMediator
            .Setup(mediator => mediator.Send(
                It.IsAny<GetAddressByCoordinatesQuery>(),
                It.IsAny<CancellationToken>()))
            .Throws<Exception>();

        var controllerResult = await controller.Nearest(latitude, longitude) as StatusCodeResult;

        controllerResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}
