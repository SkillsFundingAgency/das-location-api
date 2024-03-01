using System;
using System.Collections.Generic;
using System.Linq;
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
using SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.Postcodes;

public class WhenCallingPostBulkPostcode
{
    [Test, MoqAutoData]
    public async Task Then_Gets_Postcode_Data_From_Mediator(
        List<string> postCodes,
        GetBulkPostcodesQueryResult queryResult,
        [Frozen] Mock<IMediator> mockMediator,
        [Greedy] PostcodesController controller)
    {
        mockMediator
            .Setup(mediator => mediator.Send(
                It.Is<GetBulkPostcodesQuery>(request =>
                    request.Postcodes == postCodes),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        var controllerResult = await controller.BulkPostcode(postCodes) as ObjectResult;

        var model = controllerResult.Value as GetLocationsListResponse;
        controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        model.Locations.Should().BeEquivalentTo(queryResult.PostCodes.Select(c => (GetLocationsListItem)c).ToList(),
            options => options.ExcludingMissingMembers());
    }

    [Test, MoqAutoData]
    public async Task Then_If_An_Error_Returns_InternalServerError_Response(
        List<string> postCodes,
        [Frozen] Mock<IMediator> mockMediator,
        [Greedy] PostcodesController controller)
    {
        mockMediator
            .Setup(mediator => mediator.Send(
                It.IsAny<GetBulkPostcodesQuery>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());
        
        var controllerResult = await controller.BulkPostcode(postCodes) as StatusCodeResult;

        controllerResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}