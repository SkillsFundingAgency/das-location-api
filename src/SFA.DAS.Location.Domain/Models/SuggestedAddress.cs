using SFA.DAS.Location.Domain.ImportTypes;
using System.Linq;

namespace SFA.DAS.Location.Domain.Models
{
    public class SuggestedAddress
    {
        public string Uprn { get; set; }
        public string Organisation { get; set; }
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
                Organisation = source.OrganisationName ?? string.Empty,
                House = string.Join(", ", (new string[] { source.BuildingName, source.BuildingNumber }).Where(s => !string.IsNullOrEmpty(s))),
                Street = string.Join(", ", (new string[] { source.ThoroughfareName, source.DependentThoroughfareName }).Where(s => !string.IsNullOrEmpty(s))),
                Locality = string.Join(", ", (new string[] { source.DependentLocality, source.DoubleDependentLocality }).Where(s => !string.IsNullOrEmpty(s))),
                PostTown = source.PostTown,
                County = string.Empty, // Unable to get county from OsPlaces API as county was not required for Uk postal addresses from 1997 onwards
                Postcode = source.Postcode,
                Longitude = source.Lng,
                Latitude = source.Lat,
                Match = source.Match
            };
        }
    }

}
