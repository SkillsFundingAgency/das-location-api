using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Search.Queries.SearchLocations;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Application.UnitTests.Search.Queries
{
    public class WhenHandlingTheSearchLocationRequest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called(
            string searchTerm,
            GetLocationsQuery query,
            List<Domain.Entities.Location> locations,
            [Frozen] Mock<ILocationService> service,
            GetLocationsQueryHandler handler)
        {
            //Arrange
            query.Query = searchTerm;
            service.Setup(x => x.GetLocationsByQuery(query.Query, query.ResultCount)).ReturnsAsync(locations);
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            //Assert
            actual.SuggestedLocations.Should().BeEquivalentTo(locations.Select(c => (SuggestedLocation)c));
        }
    }
}