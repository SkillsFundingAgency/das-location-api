using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.UnitTests.Models
{
    public class WhenMappingLpiResultPlacesApiItemToSuggestedAddress
    {
        [TestCaseSource(nameof(TestCases))]
        public void Then_The_Fields_Are_Correctly_Mapped_From_LpiResultPlacesApiItem(DpaResultPlacesApiItem input, SuggestedAddress output)
        {
            DpaResultPlacesApiItem source = new DpaResultPlacesApiItem
            {
                Uprn = input.Uprn,
                BuildingName = input.BuildingName,
                BuildingNumber = input.BuildingNumber,
                ThoroughfareName = input.ThoroughfareName,
                DependentThoroughfareName = input.DependentThoroughfareName,
                DependentLocality = input.DependentLocality,
                DoubleDependentLocality = input.DoubleDependentLocality,
                PostTown = input.PostTown,
                Postcode = input.Postcode,
                Lat = input.Lat,
                Lng = input.Lng,
                Match = input.Match
            };

            var actual = (SuggestedAddress) source;
            actual.House.Should().Be(output.House);
            actual.Street.Should().Be(output.Street);
            actual.Locality.Should().Be(output.Locality);
            actual.PostTown.Should().Be(output.PostTown);
            actual.County.Should().Be(output.County);
            actual.Postcode.Should().Be(output.Postcode);
            actual.Latitude.Should().Be(output.Latitude);
            actual.Longitude.Should().Be(output.Longitude);
            actual.Match.Should().Be(output.Match);
        }

        static object[] TestCases =
        {
            new object[] 
                { 
                    new DpaResultPlacesApiItem { Uprn = "12345", BuildingName = "BuildingName", BuildingNumber = "BuildingNumber", ThoroughfareName = "ThoroughfareName", DependentThoroughfareName = "DependentThoroughfareName", DependentLocality = "DependentLocality", DoubleDependentLocality = "DoubleDependentLocality", PostTown = "PostTown", Postcode = "Postcode", Lat = 0.00000001, Lng = 3.00000001, Match = 0.44 }, 
                    new SuggestedAddress { Uprn = "12345", House = "BuildingName, BuildingNumber", Street = "ThoroughfareName, DependentThoroughfareName", Locality = "DependentLocality, DoubleDependentLocality", PostTown = "PostTown", Postcode = "Postcode", County = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 }
                },
            new object[]
                {
                    new DpaResultPlacesApiItem { Uprn = "12345", BuildingName = "BuildingName", BuildingNumber = null, ThoroughfareName = "ThoroughfareName", DependentThoroughfareName = null, DependentLocality = "DependentLocality", DoubleDependentLocality = null, PostTown = "PostTown", Postcode = "Postcode", Lat = 0.00000001, Lng = 3.00000001, Match = 0.44 },
                    new SuggestedAddress { Uprn = "12345", House = "BuildingName", Street = "ThoroughfareName", Locality = "DependentLocality", PostTown = "PostTown", Postcode = "Postcode", County = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 }
                },
            new object[]
                {
                    new DpaResultPlacesApiItem { Uprn = "12345", BuildingName = "BuildingName", BuildingNumber = null, ThoroughfareName = null, DependentThoroughfareName = null, DependentLocality = "DependentLocality", DoubleDependentLocality = "DoubleDependentLocality", PostTown = "PostTown", Postcode = "Postcode", Lat = 0.00000001, Lng = 3.00000001, Match = 0.44 },
                    new SuggestedAddress { Uprn = "12345", House = "BuildingName", Street = string.Empty, Locality = "DependentLocality, DoubleDependentLocality", PostTown = "PostTown", Postcode = "Postcode", County = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 }
                },
            new object[]
                {
                    new DpaResultPlacesApiItem { Uprn = "12345", BuildingName = null, BuildingNumber = null, ThoroughfareName = "ThoroughfareName", DependentThoroughfareName = "DependentThoroughfareName", DependentLocality = "DependentLocality", DoubleDependentLocality = "DoubleDependentLocality", PostTown = "PostTown", Postcode = "Postcode", Lat = 0.00000001, Lng = 3.00000001, Match = 0.44 },
                    new SuggestedAddress { Uprn = "12345", House = string.Empty, Street = "ThoroughfareName, DependentThoroughfareName", Locality = "DependentLocality, DoubleDependentLocality", PostTown = "PostTown", Postcode = "Postcode", County = string.Empty, Latitude = 0.00000001, Longitude = 3.00000001, Match = 0.44 }
                },
        };
    }
}