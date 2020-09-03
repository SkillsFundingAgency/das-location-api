using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Location.Services;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Application.UnitTests.Location.Services
{
    public class WhenGettingLocationsByLocationAndAuthorityName
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_With_The_Names(
            string authorityName,
            string locationName,
            Domain.Entities.Location location,
            [Frozen] Mock<ILocationRepository> repository,
            LocationService service
        )
        {
            //Arrange
            repository.Setup(x => x.GetByLocationAndAuthorityName(locationName, authorityName))
                .ReturnsAsync(location);
            
            //Act
            var actual = await service.GetLocationsByLocationAuthorityName(locationName, authorityName);
            
            //Assert
            actual.Should().BeEquivalentTo(location);
        }
    }
}