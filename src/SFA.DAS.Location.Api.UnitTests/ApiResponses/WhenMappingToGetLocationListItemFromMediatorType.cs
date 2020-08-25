using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Location.Queries;
using SFA.DAS.Location.Application.Location.Queries.SearchLocations;

namespace SFA.DAS.Location.Api.UnitTests.ApiResponses
{
    public class WhenMappingToGetLocationListItemFromMediatorType
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(GetLocationsQueryResult source)
        {
            var location = source.Locations.FirstOrDefault();
            
            var actual = (GetLocationsListItem) location;
            
            actual.Should().BeEquivalentTo(location, options=> options.ExcludingMissingMembers());
            actual.Location.Coordinates.FirstOrDefault().Should().Be(location.Lat);
            actual.Location.Coordinates.LastOrDefault().Should().Be(location.Long);
            GetLocationsListItem.Geometry.Type.Should().Be("Point");
        }
    }
}