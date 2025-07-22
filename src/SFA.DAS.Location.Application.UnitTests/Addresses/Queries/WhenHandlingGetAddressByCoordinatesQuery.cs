using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Addresses.AddressByCoordinates;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Addresses.Queries;
[TestFixture]
public class WhenHandlingGetAddressByCoordinatesQuery
{
    [Test, MoqAutoData]
    public async Task Then_The_Service_Is_Called(
        GetAddressByCoordinatesQuery query,
        SuggestedPlace address,
        [Frozen] Mock<IAddressesService> service,
        GetAddressByCoordinatesQueryHandler handler)
    {
        //Arrange
        service.Setup(x => x.NearestFromDpaDataset($"{query.Latitude},{query.Longitude}", query.Radius)).ReturnsAsync(address);
        //Act
        var actual = await handler.Handle(query, CancellationToken.None);
        //Assert
        actual.Place.Should().BeEquivalentTo(address);
    }
}