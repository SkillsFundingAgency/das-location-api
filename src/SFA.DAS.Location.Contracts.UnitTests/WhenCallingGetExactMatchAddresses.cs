using System.Net;
using AutoFixture.NUnit4;
using FluentAssertions;
using Moq;
using SFA.DAS.Apim.Shared.Models;
using SFA.DAS.Location.Contracts.ApiRequests;
using SFA.DAS.Location.Contracts.ApiResponses;
using SFA.DAS.Location.Contracts.Client;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Contracts.UnitTests;

public class WhenCallingGetExactMatchAddresses
{
    [Test, MoqAutoData]
    public async Task When_Postcode_Is_Invalid_Then_Returns_Null(
        [Frozen] Mock<ILocationApiClient<LocationApiConfiguration>> locationApiClientMock,
        LocationLookupService locationLookupService)
    {
        var result = await locationLookupService.GetExactMatchAddresses("rubbish");
        Assert.That(result, Is.Null);
        locationApiClientMock.Verify(a => a.GetWithResponseCode<GetAddressesListResponse>(It.IsAny<GetAddressesApiRequest>()), Times.Never);
    }

    [Test, MoqAutoData]
    public async Task When_Postcode_Is_Valid_Then_Calls_Api_With_Exact_Match_Param(
        GetAddressesListResponse expectedResponse,
        [Frozen] Mock<ILocationApiClient<LocationApiConfiguration>> locationApiClientMock,
        LocationLookupService locationLookupService)
    {
        var apiResponse = new ApiResponse<GetAddressesListResponse>(expectedResponse, HttpStatusCode.OK, null);
        locationApiClientMock.Setup(a => a.GetWithResponseCode<GetAddressesListResponse>(It.IsAny<GetAddressesApiRequest>())).ReturnsAsync(apiResponse);

        var result = await locationLookupService.GetExactMatchAddresses("CV1 2WT");
        locationApiClientMock.Verify(a => a.GetWithResponseCode<GetAddressesListResponse>(It.IsAny<GetAddressesApiRequest>()));
        result.Should().Be(expectedResponse);
    }

    [Test, MoqAutoData]
    public async Task When_Api_Does_Not_Return_Success_Code_Throws_Exception(
        [Frozen] Mock<ILocationApiClient<LocationApiConfiguration>> locationApiClientMock,
        LocationLookupService locationLookupService)
    {
        var apiResponse = new ApiResponse<GetAddressesListResponse>(null!, HttpStatusCode.BadRequest, "Error");
        locationApiClientMock.Setup(a => a.GetWithResponseCode<GetAddressesListResponse>(It.IsAny<GetAddressesApiRequest>())).ReturnsAsync(apiResponse);

        Func<Task> action = () => locationLookupService.GetExactMatchAddresses("CV1 2WT");

        await action.Should().ThrowAsync<InvalidOperationException>();

    }
}