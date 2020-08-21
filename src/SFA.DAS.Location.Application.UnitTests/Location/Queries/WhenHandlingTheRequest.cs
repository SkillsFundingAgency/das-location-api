using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Location.Queries;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Application.UnitTests.Location.Queries
{
    public class WhenHandlingTheRequest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called(
            GetLocationsQuery query,
            List<Domain.Entities.Location> locations,
            [Frozen] Mock<ILocationService> service,
            GetLocationsQueryHandler handler)
        {
            //Arrange
            service.Setup(x => x.GetLocationsByQuery(query.Query, query.ResultCount)).ReturnsAsync(locations);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            
            //Assert
            actual.Locations.Should().BeEquivalentTo(locations);
        }
    }
}