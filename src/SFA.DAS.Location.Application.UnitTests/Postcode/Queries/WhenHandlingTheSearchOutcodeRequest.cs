using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Location.Queries.SearchLocations;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Queries
{
    public class WhenHandlingTheSearchOutcodeRequest
    {
        [Test]
        [MoqInlineAutoData("AA9A")]
        [MoqInlineAutoData("A9A")]
        [MoqInlineAutoData("A9")]
        [MoqInlineAutoData("A99")]
        [MoqInlineAutoData("AA9")]
        [MoqInlineAutoData("AA99")]
        public async Task Then_The_Service_Is_Called(
            string searchTerm,
            GetLocationsQuery query,
            IEnumerable<SuggestedLocation> locations,
            [Frozen] Mock<IPostcodeService> service,
            GetLocationsQueryHandler handler)
        {
            //Arrange
            query.Query = searchTerm;
            service.Setup(x => x.GetPostcodeByOutcodeQuery(query.Query, query.ResultCount)).ReturnsAsync(locations);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);

            //Assert
            actual.SuggestedLocations.Should().BeEquivalentTo(locations);
        }
    }
}
