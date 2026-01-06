using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Api.Controllers.V2;
using SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.V2.Postcodes;
[TestFixture]
internal class WhenCallingPostBulkPostcode
{
    [Test, MoqAutoData]
    public async Task Then_Gets_Postcode_Data_From_Mediator_And_Null_Values_Filtered(
        List<string> postCodes,
        GetBulkPostcodesQueryV2Result queryResult,
        [Frozen] Mock<IMediator> mockMediator,
        [Greedy] PostcodesController controller)
    {
        queryResult.PostCodes.Add(null);
        mockMediator
            .Setup(mediator => mediator.Send(
                It.Is<GetBulkPostcodesQueryV2>(request =>
                    request.Postcodes == postCodes),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        var controllerResult = await controller.BulkPostcode(postCodes) as ObjectResult;

        var model = controllerResult.Value as GetLocationsListResponse;
        controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        model.Locations.Should().BeEquivalentTo(queryResult.PostCodes.Where(c => c != null).Select(c => (GetLocationsListItem)c).ToList(),
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
                It.IsAny<GetBulkPostcodesQueryV2>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        var controllerResult = await controller.BulkPostcode(postCodes) as StatusCodeResult;

        controllerResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}
