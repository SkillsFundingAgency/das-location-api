using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient;

public class WhenGettingLookupPostcode
{
    [Test, MoqAutoData]
    public async Task Then_The_Result_Is_Mapped_Correctly(
        string query,
        List<SuggestedAddress> suggestedAddresses,
        [Frozen] Mock<IOsPlacesApiService> apiService,
        PostCodeApiV2Service sut)
    {
        // arrange
        apiService
            .Setup(x => x.FindFromDpaOsPlaces(query, 1, 1.0, CancellationToken.None))
            .ReturnsAsync(suggestedAddresses);

        var expectedResult = PostcodeDataV2.From(suggestedAddresses[0]);
        
        // act
        var result = await sut.LookupPostcodeAsync(query, CancellationToken.None);
    
        // assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Test, MoqAutoData]
    public async Task Then_If_The_Postcode_Is_Not_Found_Null_Is_Returned(
        string query,
        [Frozen] Mock<IOsPlacesApiService> apiService,
        PostCodeApiV2Service sut)
    {
        // arrange
        apiService
            .Setup(x => x.FindFromDpaOsPlaces(query, 1, 1.0, CancellationToken.None))
            .ReturnsAsync(new List<SuggestedAddress>());
        
        // act
        var result = await sut.LookupPostcodeAsync(query, CancellationToken.None);

        // assert
        result.Should().BeNull();
    }
}