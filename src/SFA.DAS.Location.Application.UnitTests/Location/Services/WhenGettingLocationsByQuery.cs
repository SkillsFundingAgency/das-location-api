using System.Collections.Generic;
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
    public class WhenGettingLocationsByQuery
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_With_The_Query_And_Count(
            int resultCount,
            string query,
            List<Domain.Entities.Location> locations,
            [Frozen] Mock<ILocationRepository> repository,
            LocationService service
            )
        {
            //Arrange
            repository.Setup(x => x.GetAllStartingWith(query, resultCount))
                .ReturnsAsync(locations);
            
            //Act
            var actual = await service.GetLocationsByQuery(query, resultCount);
            
            //Assert
            actual.Should().BeEquivalentTo(locations);
        }
    }
}