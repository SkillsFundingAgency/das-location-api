using System.Threading;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Api.Controllers.V2;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.V2.Postcodes;

public class WhenGettingLookupPostcode
{
    [Test, MoqAutoData]
    public async Task Then_The_Result_Is_Returned_Successfully(
        string postcode,
        PostcodeDataV2 result,
        CancellationToken cancellationToken,
        Mock<IPostcodeApiV2Service> postcodeService,
        [Greedy] PostcodesController sut)
    {
        // arrange
        postcodeService.Setup(x => x.LookupPostcodeAsync(postcode, cancellationToken)).ReturnsAsync(result);

        // act
        var response = await sut.LookupPostcode(postcode, postcodeService.Object, cancellationToken) as Ok<LookupPostcodeV2Response>;

        // assert
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        response.Value.Should().BeEquivalentTo(LookupPostcodeV2Response.From(result));
    }
    
    [Test, MoqAutoData]
    public async Task Then_NotFound_Is_Returned_If_No_Result_Is_Found(
        string postcode,
        CancellationToken cancellationToken,
        Mock<IPostcodeApiV2Service> postcodeService,
        [Greedy] PostcodesController sut)
    {
        // arrange
        postcodeService.Setup(x => x.LookupPostcodeAsync(postcode, cancellationToken)).ReturnsAsync((PostcodeDataV2)null);

        // act
        var response = await sut.LookupPostcode(postcode, postcodeService.Object, cancellationToken) as NotFound;

        // assert
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}