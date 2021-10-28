using SFA.DAS.Location.Domain.ImportTypes;
using System.Linq;
using System.Globalization;

namespace SFA.DAS.Location.Domain.Models
{
    public class SuggestedAddress
    {
        public string Uprn { get; set; }
        public string Organisation { get; set; }
        public string Premises { get; set; }
        public string Thoroughfare  { get; set; }
        public string Locality { get; set; }
        public string PostTown { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? Match { get; set; }

        public static implicit operator SuggestedAddress(DpaResultPlacesApiItem source)
        {
            // thoroughfare parts are joined with a comma but the bulding number will be prepended with a space
            var thoroughfare = string.Join(", ", (new string[] { source.DependentThoroughfareName, source.ThoroughfareName }).Where(s => !string.IsNullOrEmpty(s)));

            return new SuggestedAddress
            {
                Uprn = source.Uprn,
                Organisation = ToCamelCase(source.OrganisationName ?? string.Empty),
                Premises = ToCamelCase(string.Join(", ", (new string[] { source.SubBuildingName, source.BuildingName }).Where(s => !string.IsNullOrEmpty(s)))),
                Thoroughfare  = ToCamelCase(string.Join(" ", (new string[] { source.BuildingNumber, thoroughfare }).Where(s => !string.IsNullOrEmpty(s)))),
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
