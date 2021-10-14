using SFA.DAS.Location.Domain.ImportTypes;
using System.Linq;
using System.Globalization;

namespace SFA.DAS.Location.Domain.Models
{
    public class SuggestedAddress
    {
        public string Uprn { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string PostTown { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? Match { get; set; }

        public static implicit operator SuggestedAddress(DpaResultPlacesApiItem source)
        {
            return new SuggestedAddress
            {
                Uprn = source.Uprn,
                House = ToCamelCase(string.Join(", ", (new string[] { source.BuildingName, source.BuildingNumber }).Where(s => !string.IsNullOrEmpty(s)))),
                Street = ToCamelCase(string.Join(", ", (new string[] { source.DependentThoroughfareName, source.ThoroughfareName }).Where(s => !string.IsNullOrEmpty(s)))),
                Locality = ToCamelCase(string.Join(", ", (new string[] { source.DoubleDependentLocality, source.DependentLocality }).Where(s => !string.IsNullOrEmpty(s)))),
                PostTown = ToCamelCase(source.PostTown),
                County = string.Empty, // Unable to get county from OsPlaces API as county was not required for Uk postal addresses from 1997 onwards
                Postcode = source.Postcode,
                Longitude = source.Lng,
                Latitude = source.Lat,
                Match = source.Match
            };
        }

        private static string ToCamelCase(string input)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }
    }

}
