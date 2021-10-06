using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.UnitTests.Models
{
    public class WhenMappingLpiResultPlacesApiItemToSuggestedAddress
    {
        [TestCaseSource(nameof(TestCases))]
        public void Then_The_Fields_Are_Correctly_Mapped_From_LpiResultPlacesApiItem(string uprn, string paoText, string paoStartNumber, string streetDescription, 
            string townName, string postCodeLocator, double lat, double lng, double match)
        {
            LpiResultPlacesApiItem source = new LpiResultPlacesApiItem
            {
                Uprn = uprn,
                PaoText = paoText,
                PaoStartNumber = paoStartNumber,
                StreetDescription = streetDescription,
                TownName = townName,
                PostCodeLocator = postCodeLocator,
                Lat = lat,
                Lng = lng,
                Match = match
            };

            var actual = (SuggestedAddress) source;
            if(!string.IsNullOrEmpty(source.PaoText))
            {
                actual.AddressLine1.Should().Be($"{source.PaoText + ", " + source.PaoStartNumber}");
            }
            else
            {
                actual.AddressLine1.Should().Be(source.PaoStartNumber);
            }

            actual.AddressLine2.Should().Be(source.StreetDescription);
            actual.Town.Should().Be(source.TownName);
            actual.Postcode.Should().Be(source.PostCodeLocator);
            actual.Latitude.Should().Be(source.Lat);
            actual.Longitude.Should().Be(source.Lng);
            actual.Match.Should().Be(source.Match);
        }

        static object[] TestCases =
        {
            new object[] { "12345", "The Dome", "1", "Fuller Street", "Buckminster", "BM1 7YI", 0.23737578, 3.18373737, 4.3879 },
            new object[] { "12345", null, "1", "Fuller Street", "Buckminster", "BM1 7YI", 0.23737578, 3.18373737, 4.3879 },
            new object[] { "12345", "", "1", "Fuller Street", "Buckminster", "BM1 7YI", 0.23737578, 3.18373737, 4.3879 },
        };
    }
}