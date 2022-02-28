using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Application.Location.Queries.GetByLocationAuthorityName;
using SFA.DAS.Location.Application.Postcode.Queries.GetByFullPostcode;
using SFA.DAS.Location.Application.Search.Queries.SearchLocalAuthority;
using SFA.DAS.Location.Application.Search.Queries.SearchLocations;

namespace SFA.DAS.Location.Api.UnitTests.ApiResponses
{
    public class WhenMappingToGetLocationListItemFromMediatorType
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped_From_GetLocationsQueryResult(GetLocationsQueryResult source)
        {
            var location = source.SuggestedLocations.FirstOrDefault();
            
            var actual = (GetLocationsListItem) location;
            
            actual.Should().BeEquivalentTo(location, options=> options.ExcludingMissingMembers());
            actual.Location.Coordinates.FirstOrDefault().Should().Be(location.Lat);
            actual.Location.Coordinates.LastOrDefault().Should().Be(location.Long);
            actual.Country.Should().Be(location.Country);
            GetLocationsListItem.Geometry.Type.Should().Be("Point");
        }

        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped_From_GetPostcodeQueryResult(GetPostcodeQueryResult source)
        {
            var postcode = source.Postcode;

            var actual = (GetLocationsListItem)postcode;

            actual.Should().BeEquivalentTo(postcode, options => options.ExcludingMissingMembers());
            actual.Location.Coordinates.FirstOrDefault().Should().Be(postcode.Lat);
            actual.Location.Coordinates.LastOrDefault().Should().Be(postcode.Long);
            actual.DistrictName.Should().Be(source.Postcode.AdminDistrict);
            actual.Country.Should().Be(source.Postcode.Country);
            GetLocationsListItem.Geometry.Type.Should().Be("Point");
        }

        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped_From_GetLocationQueryResult(GetLocationQueryResult source)
        {
            var location = source.Location;

            var actual = (GetLocationsListItem)location;

            actual.Should().BeEquivalentTo(location, options => options.ExcludingMissingMembers());
            actual.Location.Coordinates.FirstOrDefault().Should().Be(location.Lat);
            actual.Location.Coordinates.LastOrDefault().Should().Be(location.Long);
            GetLocationsListItem.Geometry.Type.Should().Be("Point");
        }

        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped_From_GetLocationsQueryResult(GetLocalAuthoritySearchQueryResult source)
        {
            var location = source.SuggestedLocations.FirstOrDefault();

            var actual = (GetLocationsListItem)location;

            actual.Should().BeEquivalentTo(location, options => options.ExcludingMissingMembers());
            actual.Location.Coordinates.FirstOrDefault().Should().Be(location.Lat);
            actual.Location.Coordinates.LastOrDefault().Should().Be(location.Long);
            actual.Country.Should().Be(location.Country);
            GetLocationsListItem.Geometry.Type.Should().Be("Point");
            actual.LocalAuthorityName.Should().Be(location.LocalAuthorityName);
            actual.LocalAuthorityDistrict.Should().Be(location.LocalAuthorityDistrict);
            actual.Region.Should().Be(location.Region);
        }
    }
}