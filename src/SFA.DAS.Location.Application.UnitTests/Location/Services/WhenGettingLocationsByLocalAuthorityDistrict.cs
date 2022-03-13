using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Location.Services;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Location.Services
{
    public class WhenGettingLocationsByLocalAuthorityDistrict
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_With_The_Query_And_Count(
            string query,
            List<Domain.Entities.Location> locations,
            [Frozen] Mock<ILocationRepository> repository,
            LocationService service
            )
        {
            //Arrange
            repository.Setup(x => x.GetByLocalAuthorityDistrict(query))
                .ReturnsAsync(locations);

            //Act
            var actual = await service.GetLocationsByLocalAuthorityDistrict(query);

            //Assert
            actual.Should().BeEquivalentTo(locations);
        }
    }
}
