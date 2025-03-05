using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Api.ApiResponses;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Api.UnitTests.ApiResponses;

public class WhenMappingToGetAddressListItemFromMediatorType
{
    [TestCaseSource(nameof(TestCases))]
    public void Then_The_Fields_Are_Correctly_Mapped_From_GetLocationsQueryResult(SuggestedAddress input, GetAddressesListItem output)
    {
        // act
        var actual = GetAddressesListItem.From(input);
            
        // assert
        actual.Should().BeEquivalentTo(output, options => options.ExcludingMissingMembers());
    }

    static readonly object[] TestCases =
    [
        new object[]
        {
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = "Subbuildingname, Buildingname", Thoroughfare  = "Buildingnumber Dependentthoroughfarename, Thoroughfarename", Locality = "Doubledependentlocality, Dependentlocality", PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE", Country = "England", Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 },
            new GetAddressesListItem { Uprn = "12345", Organisation = "Organisation", Premises = "Subbuildingname, Buildingname", Thoroughfare = "Buildingnumber Dependentthoroughfarename, Thoroughfarename", Locality = "Doubledependentlocality, Dependentlocality", PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE", Country = "England", AddressLine1 = "Subbuildingname, Buildingname", AddressLine2 = "Buildingnumber Dependentthoroughfarename, Thoroughfarename", AddressLine3 = "Doubledependentlocality, Dependentlocality", Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44,  }
        },
        new object[]
        {
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = "Subbuildingname, Buildingname", Thoroughfare  = "Thoroughfarename", Locality = string.Empty, PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE", Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 },
            new GetAddressesListItem { Uprn = "12345", Organisation = "Organisation", Premises = "Subbuildingname, Buildingname", Thoroughfare = "Thoroughfarename", Locality = string.Empty, PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE",  AddressLine1 = "Subbuildingname, Buildingname", AddressLine2 = "Thoroughfarename", AddressLine3 = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44,  }
        },
        new object[]
        {
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = "Buildingname", Thoroughfare  = string.Empty, Locality = "Doubledependentlocality, Dependentlocality", PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE", Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 },
            new GetAddressesListItem { Uprn = "12345", Organisation = "Organisation", Premises = "Buildingname", Thoroughfare = string.Empty, Locality = "Doubledependentlocality, Dependentlocality", PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE",  AddressLine1 = "Buildingname", AddressLine2 = "Doubledependentlocality, Dependentlocality", AddressLine3 = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44,  }
        },
        new object[]
        {
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = string.Empty, Thoroughfare  = "Buildingnumber Thoroughfarename", Locality = "Dependentlocality", PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE", Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 },
            new GetAddressesListItem { Uprn = "12345", Organisation = "Organisation", Premises = string.Empty, Thoroughfare = "Buildingnumber Thoroughfarename", Locality = "Dependentlocality", PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE",  AddressLine1 = "Buildingnumber Thoroughfarename", AddressLine2 = "Dependentlocality", AddressLine3 = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44,  }
        },
        new object[]
        {
            // majority digit rollup to thoroughfare
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = "11A", Thoroughfare  = "Thoroughfarename", Locality = string.Empty, PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE", Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 },
            new GetAddressesListItem { Uprn = "12345", Organisation = "Organisation", Premises = "11A", Thoroughfare = "Thoroughfarename", Locality = string.Empty, PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE",  AddressLine1 = "11A Thoroughfarename", AddressLine2 = string.Empty, AddressLine3 = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44,  }
        },
        new object[]
        {
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = "Buildingname", Thoroughfare  = "Thoroughfarename", Locality = string.Empty, PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE", Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 },
            new GetAddressesListItem { Uprn = "12345", Organisation = "Organisation", Premises = "Buildingname", Thoroughfare = "Thoroughfarename", Locality = string.Empty, PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE",  AddressLine1 = "Buildingname", AddressLine2 = "Thoroughfarename", AddressLine3 = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44,  }
        },
        new object[]
        {
            // majority digit rollup to locality
            new SuggestedAddress { Uprn = "12345", Organisation = "Organisation", Premises = "11-12", Thoroughfare  = string.Empty, Locality = "Dependentlocality", PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE", Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 },
            new GetAddressesListItem { Uprn = "12345", Organisation = "Organisation", Premises = "11-12", Thoroughfare = string.Empty, Locality = "Dependentlocality", PostTown = "Posttown", County = string.Empty, Postcode = "POSTCODE",  AddressLine1 = "11-12 Dependentlocality", AddressLine2 = string.Empty, AddressLine3 = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44,  }
        }
    ];
}