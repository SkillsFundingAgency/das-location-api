using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Search.Queries.SearchLocations;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Search.Queries
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
        public async Task Then_The_Service_Is_Called_And_District_First(
            string searchTerm,
            GetLocationsQuery query,
            List<SuggestedLocation> locations,
            SuggestedLocation district,
            [Frozen] Mock<IPostcodeService> service,
            GetLocationsQueryHandler handler)
        {
            //Arrange
            query.Query = searchTerm;
            service.Setup(x => x.GetPostcodeByOutcodeQuery(query.Query, query.ResultCount)).ReturnsAsync(locations);
            service.Setup(y => y.GetDistrictNameByOutcodeQuery(query.Query)).ReturnsAsync(district);

            //Act
            var actual = await handler.Handle(query, CancellationToken.None);

            //Assert
            actual.SuggestedLocations.Should().Contain(locations, district);
            actual.SuggestedLocations.First().Should().Be(district);
        }

        [Test, MoqInlineAutoData("AA")]
        public async Task If_Query_Is_Two_Letter_String_Then_Service_Is_Not_Called(
            string searchTerm,
            GetLocationsQuery query,
            List<SuggestedLocation> locations,
            SuggestedLocation district,
            [Frozen] Mock<IPostcodeService> service,
            GetLocationsQueryHandler handler)
        {
            //Arrange
            query.Query = searchTerm;
            service.Setup(x => x.GetPostcodeByOutcodeQuery(query.Query, query.ResultCount)).ReturnsAsync(locations);
            service.Setup(y => y.GetDistrictNameByOutcodeQuery(query.Query)).ReturnsAsync(district);

            //Act
            var actual = await handler.Handle(query, CancellationToken.None);

            //Assert 
            service.Verify(x => x.GetPostcodeByOutcodeQuery(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            service.Verify(y => y.GetDistrictNameByOutcodeQuery(It.IsAny<string>()), Times.Never);

            actual.SuggestedLocations.Should().BeNull();
        }
        
        [Test]
         [MoqInlineAutoData("AV1")]
        [MoqInlineAutoData("AV1 1")]
        public async Task Then_If_No_District_Is_Returned_It_Is_Not_Added(
            string searchTerm,
            GetLocationsQuery query,
            List<SuggestedLocation> locations,
            SuggestedLocation district,
            [Frozen] Mock<IPostcodeService> service,
            GetLocationsQueryHandler handler)
        {
            //Arrange
            query.Query = searchTerm;
            service.Setup(x => x.GetPostcodeByOutcodeQuery(query.Query, query.ResultCount)).ReturnsAsync(locations);
            service.Setup(y => y.GetDistrictNameByOutcodeQuery(query.Query)).ReturnsAsync((SuggestedLocation) null);

            //Act
            var actual = await handler.Handle(query, CancellationToken.None);

            //Assert
            actual.SuggestedLocations.Should().Contain(locations);
            actual.SuggestedLocations.Should().NotContainNulls();
        }
    }
}
