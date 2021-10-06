using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Addresses.Queries;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Search.Queries
{
    public class WhenHandlingTheGetAddressesRequest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called(
            string searchTerm,
            GetAddressesQuery query,
            List<SuggestedAddress> addresses,
            [Frozen] Mock<IAddressesService> service,
            GetAddressesQueryHandler handler)
        {
            //Arrange
            query.Query = searchTerm;
            service.Setup(x => x.FindFromLpiDataset(query.Query, query.MinMatch)).ReturnsAsync(addresses);
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            //Assert
            actual.Addresses.Should().BeEquivalentTo(addresses);
        }
    }
}