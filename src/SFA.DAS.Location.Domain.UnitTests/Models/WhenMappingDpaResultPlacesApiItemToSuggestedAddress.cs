using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.UnitTests.Models;

public class WhenMappingDpaResultPlacesApiItemToSuggestedAddress
{
    [TestCaseSource(nameof(TestCases))]
    public void Then_The_Fields_Are_Correctly_Mapped_From_DpaResultPlacesApiItem(DpaResultPlacesApiItem input, SuggestedAddress output)
    {
        // act
        var actual = SuggestedAddress.From(input);
            
        // assert
        actual.Organisation.Should().Be(output.Organisation);
        actual.Premises.Should().Be(output.Premises);
        actual.Thoroughfare .Should().Be(output.Thoroughfare );
        actual.Locality.Should().Be(output.Locality);
        actual.PostTown.Should().Be(output.PostTown);
        actual.County.Should().Be(output.County);
        actual.Postcode.Should().Be(output.Postcode);
        actual.Latitude.Should().Be(output.Latitude);
        actual.Longitude.Should().Be(output.Longitude);
        actual.Match.Should().Be(output.Match);
    }

    static readonly object[] TestCases =
    [
        new object[] 
        { 
            new DpaResultPlacesApiItem { Uprn = "12345", OrganisationName = "Organisation", SubBuildingName = "SubBuildingName", BuildingName = "BuildingName", BuildingNumber = "BuildingNumber", ThoroughfareName = "ThoroughfareName", DependentThoroughfareName = "DependentThoroughfareName", DependentLocality = "DependentLocality", DoubleDependentLocality = "DoubleDependentLocality", PostTown = "PostTown", Postcode = "POSTCODE", Lat = 0.00000001, Lng = 3.00000001, Match = 0.44 }, 
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = "Subbuildingname, Buildingname", Thoroughfare  = "Buildingnumber Dependentthoroughfarename, Thoroughfarename", Locality = "Doubledependentlocality, Dependentlocality", PostTown = "Posttown", Postcode = "POSTCODE", County = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 }
        },
        new object[]
        {
            new DpaResultPlacesApiItem { Uprn = "12345", OrganisationName = "Organisation",  SubBuildingName = null, BuildingName = "BuildingName", BuildingNumber = null, ThoroughfareName = "ThoroughfareName", DependentThoroughfareName = null, DependentLocality = "DependentLocality", DoubleDependentLocality = null, PostTown = "PostTown", Postcode = "POSTCODE", Lat = 0.00000001, Lng = 3.00000001, Match = 0.44 },
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = "Buildingname", Thoroughfare  = "Thoroughfarename", Locality = "Dependentlocality", PostTown = "Posttown", Postcode = "POSTCODE", County = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 }
        },
        new object[]
        {
            new DpaResultPlacesApiItem { Uprn = "12345", OrganisationName = "Organisation", SubBuildingName = null, BuildingName = "BuildingName", BuildingNumber = null, ThoroughfareName = null, DependentThoroughfareName = null, DependentLocality = "DependentLocality", DoubleDependentLocality = "DoubleDependentLocality", PostTown = "PostTown", Postcode = "POSTCODE", Lat = 0.00000001, Lng = 3.00000001, Match = 0.44 },
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = "Buildingname", Thoroughfare  = string.Empty, Locality = "Doubledependentlocality, Dependentlocality", PostTown = "Posttown", Postcode = "POSTCODE", County = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 }
        },
        new object[]
        {
            new DpaResultPlacesApiItem { Uprn = "12345", OrganisationName = "Organisation", SubBuildingName = null, BuildingName = null, BuildingNumber = null, ThoroughfareName = "ThoroughfareName", DependentThoroughfareName = "DependentThoroughfareName", DependentLocality = "DependentLocality", DoubleDependentLocality = "DoubleDependentLocality", PostTown = "PostTown", Postcode = "POSTCODE", Lat = 0.00000001, Lng = 3.00000001, Match = 0.44 },
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = string.Empty, Thoroughfare  = "Dependentthoroughfarename, Thoroughfarename", Locality = "Doubledependentlocality, Dependentlocality", PostTown = "Posttown", Postcode = "POSTCODE", County = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 }
        }
    ];

    [TestCase("E", "England")]
    [TestCase("S", "Scotland")]
    [TestCase("W", "Wales")]
    [TestCase("X", null)]
    public void Then_The_Country_Field_Is_Mapped_Correctly(string countryCode, string expectedCountry)
    {
        // arrange
        var source = new DpaResultPlacesApiItem
        {
            CountryCode = countryCode,
            PostTown = "PostTown",
        };

        // act
        var actual = SuggestedAddress.From(source);
            
        // assert
        actual.Country.Should().Be(expectedCountry);
    }
}