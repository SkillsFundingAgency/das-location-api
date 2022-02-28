using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Search.Queries.SearchLocalAuthority;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Search.Queries
{
    public class WhenHandlingTheGetLocalAuthoritySearchRequest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called(
            string searchTerm,
            GetLocalAuthoritySearchQuery query,
            List<Domain.Entities.Location> locations,
            [Frozen] Mock<ILocationService> service,
            GetLocalAuthoritySearchQueryHandler handler)
        {
            //Arrange
            query.Query = searchTerm;
            service.Setup(x => x.GetLocationsByLocalAuthoritySearch(query.Query, query.ResultCount)).ReturnsAsync(locations);
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            //Assert
            actual.SuggestedLocations.Should().BeEquivalentTo(locations.Select(c => (SuggestedLocation)c));
        }
    }
}
