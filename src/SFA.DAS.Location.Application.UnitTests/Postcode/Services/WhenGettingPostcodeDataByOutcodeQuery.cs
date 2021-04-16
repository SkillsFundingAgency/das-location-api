using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Postcode.Services;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;
using SFA.DAS.Testing.AutoFixture;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Services
{
    public class WhenGettingPostcodeDataByOutcodeQuery
    {
        [Test, MoqAutoData]
        public async Task Then_The_Outcode_API_Is_Called_With_Query(
            string query,
            PostcodeData postcode,
            [Frozen] Mock<IPostcodeApiService> postcodeApiService,
            PostcodeService postcodeService)
        {
            //Arrange
            postcodeApiService.Setup(x => x.GetFullPostcodeDataByOutcode(query))
                .ReturnsAsync(postcode);

            //Act
            var actual = await postcodeService.GetPostcodeDataByOutcode(query);

            //Assert
            actual.Should().Be(postcode);
        }
    }
}
