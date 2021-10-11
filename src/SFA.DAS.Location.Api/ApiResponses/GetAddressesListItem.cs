using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Api.ApiResponses
{
    public class GetAddressesListItem
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

        public static implicit operator GetAddressesListItem(SuggestedAddress source)
        {
            return new GetAddressesListItem
            {
                Uprn = source.Uprn,
                HouseName = source.HouseName,
                HouseNumber = source.HouseNumber,
                StreetName = source.StreetName,
                Town = source.Town,
                County = source.County,
                Postcode = source.Postcode,
                Longitude = source.Longitude,
                Latitude = source.Latitude,
                Match = source.Match
            };
        }
    }
}
