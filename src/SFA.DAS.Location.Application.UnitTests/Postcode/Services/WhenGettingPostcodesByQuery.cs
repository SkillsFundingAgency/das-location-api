using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Postcode.Services;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Services
{
    public class WhenGettingPostcodesByQuery
    {
        [Test, MoqAutoData]
        public async Task Then_The_Postcode_API_Is_Called_With_Query_And_Count(
            string query,
            PostcodeData postcode,
            [Frozen] Mock<IPostcodeApiService> postcodeApiService,
            PostcodeService postcodeService)
        {
            //Arrange
            postcodeApiService.Setup(x => x.GetPostcodeData(query))
                .ReturnsAsync(postcode);

            //Act
            var actual = await postcodeService.GetPostcodeByFullPostcode(query);

            //Assert
            actual.Should().Be(postcode);
        }
    }
}
