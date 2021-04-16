using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Postcode.Services;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Services
{
    public class WhenGettingLocationsByOutcode
    {
        [Test, MoqAutoData]
        public async Task Then_The_Api_Is_Called_With_The_Query_And_Count_And_Returns_Postcode_Data(
            int resultCount,
            string query,
            IEnumerable<SuggestedLocation> locations,
            [Frozen] Mock<IPostcodeApiService> apiService,
            PostcodeService service
            )
        {
            //Arrange
            apiService.Setup(x => x.GetAllStartingWithOutcode(query, resultCount))
                .ReturnsAsync(locations);

            //Act
            var actual = await service.GetPostcodesByOutcodeQuery(query, resultCount);

            //Assert
            actual.Should().BeEquivalentTo(locations);
        }

        [Test, MoqAutoData]
        public async Task Then_The_Outcode_Api_Is_Called_With_The_Query_And_Returns_District_Data(
            string query,
            SuggestedLocation location,
            [Frozen] Mock<IPostcodeApiService> apiService,
            PostcodeService service
            )
        {
            //Arrange
            apiService.Setup(x => x.GetDistrictData(query)).ReturnsAsync(location);

            //Act
            var actual = await service.GetDistrictNameByOutcodeQuery(query);
            actual.Lat.Should().Be(location.Lat);
            //Assert
            actual.Should().BeEquivalentTo(location);
        }
    }
}
