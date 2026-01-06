using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes.V2;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Queries;

public class WhenHandlingGetBulkPostcodesQueryV2
{
    [Test, MoqAutoData]
    public async Task Then_The_Api_Is_Called_And_Postcode_Data_Returned(
        SuggestedAddress data,
        string postcode,
        [Frozen] Mock<IAddressesService> postcodeApiService,
        [Greedy] GetBulkPostcodesQueryV2Handler handler)
    {
        var query = new GetBulkPostcodesQueryV2([postcode]);
        data.Postcode = postcode;

        postcodeApiService.Setup(x => x.FindFromDpaOsPlaces(It.IsAny<string>(), It.IsAny<double>(),It.IsAny<CancellationToken>())).ReturnsAsync([data]);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.PostCodes[0].Postcode.Should().Be(data.Postcode);
        actual.PostCodes[0].Lat.Should().Be(data.Latitude);
        actual.PostCodes[0].Long.Should().Be(data.Longitude);
        actual.PostCodes[0].Country.Should().Be(data.Country);
    }
}