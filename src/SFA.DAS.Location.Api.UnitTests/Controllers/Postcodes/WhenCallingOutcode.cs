using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Api.Controllers;
using SFA.DAS.Location.Application.Postcode.Queries.GetByFullPostcode;
using SFA.DAS.Location.Application.Postcode.Queries.GetByOutcode;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.Postcodes
{
    public class WhenCallingOutcode
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Location_From_Mediator(
            string query,
            GetOutcodeQueryResult queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] PostcodesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetOutcodeQuery>(request =>
                        request.Outcode == query),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.Outcode(query) as ObjectResult;

            var model = controllerResult.Value as GetLocationsListItem;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Should().BeEquivalentTo(queryResult.Outcode, options => options.ExcludingMissingMembers());
        }
    }
}
