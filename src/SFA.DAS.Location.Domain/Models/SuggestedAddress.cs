using SFA.DAS.Location.Domain.ImportTypes;
using System.Linq;

namespace SFA.DAS.Location.Domain.Models
{
    public class SuggestedAddress
    {
        public string Uprn { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Match { get; set; }

        public static implicit operator SuggestedAddress(LpiResultPlacesApiItem source)
        {
            return new SuggestedAddress
            {
                Uprn = source.Uprn,
                HouseName = source.PaoText,
                HouseNumber = source.PaoStartNumber,
                StreetName = source.StreetDescription,
                Town = source.TownName,
                County = string.Empty, // Unable to get county from OsPlaces API as county was not required for Uk postal addresses from 1997 onwards
                Postcode = source.PostCodeLocator,
                Longitude = source.Lng,
                Latitude = source.Lat,
                Match = source.Match
            };
        }
    }

}
