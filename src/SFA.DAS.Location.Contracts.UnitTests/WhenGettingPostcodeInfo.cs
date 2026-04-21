using System.Net;
using System.Text.Encodings.Web;
using AutoFixture.NUnit4;
using FluentAssertions;
using Moq;
using SFA.DAS.Apim.Shared.Interfaces;
using SFA.DAS.Apim.Shared.Models;
using SFA.DAS.Location.Contracts.ApiRequests.V2;
using SFA.DAS.Location.Contracts.ApiResponses.V2;
using SFA.DAS.Location.Contracts.Client;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Contracts.UnitTests;

public class WhenGettingPostcodeInfo
{
    [Test, MoqAutoData]
    public async Task Then_Null_Is_Returned_When_The_Postcode_Could_Not_Be_Found(
        string postcode,
        [Frozen] Mock<ILocationApiClient<LocationApiConfiguration>> client,
        [Greedy] LocationLookupService service)
    {
        // arrange
        var apiResponse = new ApiResponse<LookupPostcodeV2Response>(null, HttpStatusCode.NotFound, string.Empty);
        GetPostcodesApiRequest? capturedRequest = null;
        client
            .Setup(x => x.GetWithResponseCode<LookupPostcodeV2Response>(It.IsAny<GetPostcodesApiRequest>()))
            .Callback<IGetApiRequest>(x => capturedRequest = x as GetPostcodesApiRequest)
            .ReturnsAsync(apiResponse);

        // act
        var result = await service.GetPostcodeInfoAsync(postcode);

        // assert
        capturedRequest.Version.Should().Be("2.0");
        capturedRequest.GetUrl.Should().Be($"api/Postcodes?postcode={HtmlEncoder.Default.Encode(postcode)}");
        result.Should().Be(null);
    }

    [Test, MoqAutoData]
    public async Task Then_The_Info_Is_Returned_When_The_Postcode_Is_Found(
        string postcode,
        LookupPostcodeV2Response response,
        [Frozen] Mock<ILocationApiClient<LocationApiConfiguration>> client,
        [Greedy] LocationLookupService service)
    {
        // arrange
        var apiResponse = new ApiResponse<LookupPostcodeV2Response>(response, HttpStatusCode.OK, string.Empty);
        GetPostcodesApiRequest? capturedRequest = null;
        client
            .Setup(x => x.GetWithResponseCode<LookupPostcodeV2Response>(It.IsAny<GetPostcodesApiRequest>()))
            .Callback<IGetApiRequest>(x => capturedRequest = x as GetPostcodesApiRequest)
            .ReturnsAsync(apiResponse);

        // act
        var result = await service.GetPostcodeInfoAsync(postcode);

        // assert
        capturedRequest.Version.Should().Be("2.0");
        capturedRequest.GetUrl.Should().Be($"api/Postcodes?postcode={HtmlEncoder.Default.Encode(postcode)}");
        result.Should().BeEquivalentTo(response,
            opt => opt.WithMapping<LookupPostcodeV2Response, PostcodeInfo>(x => x.DistrictName, x => x.AdminDistrict));
    }
}