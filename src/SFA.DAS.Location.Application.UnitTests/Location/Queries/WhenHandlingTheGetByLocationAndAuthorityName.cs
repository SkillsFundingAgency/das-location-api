using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Location.Queries.GetByLocationAuthorityName;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Application.UnitTests.Location.Queries
{
    public class WhenHandlingTheGetByLocationAndAuthorityName
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called(
            GetLocationQuery query,
            Domain.Entities.Location location,
            [Frozen] Mock<ILocationService> service,
            GetLocationQueryHandler handler)
        {
            //Arrange
            service.Setup(x => x.GetLocationsByLocationAuthorityName(query.LocationName, query.AuthorityName)).ReturnsAsync(location);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            
            //Assert
            actual.Location.Should().BeEquivalentTo(location);
        }
    }
}