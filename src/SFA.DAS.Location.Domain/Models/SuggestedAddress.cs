using SFA.DAS.Location.Domain.ImportTypes;
using System.Linq;

namespace SFA.DAS.Location.Domain.Models
{
    public class SuggestedAddress
    {
        public string Uprn { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Match { get; set; }

        public static implicit operator SuggestedAddress(LpiResultPlacesApiItem source)
        {
            return new SuggestedAddress
            {
                Uprn = source.Uprn,
                AddressLine1 = $"{(!string.IsNullOrEmpty(source.PaoText) ? source.PaoText + ", " : string.Empty)}{source.PaoStartNumber}",
                AddressLine2 = source.StreetDescription,
                Town = source.TownName,
                Postcode = source.PostCodeLocator,
                Longitude = source.Lng,
                Latitude = source.Lat,
                Match = source.Match
            };
        }
    }

}
