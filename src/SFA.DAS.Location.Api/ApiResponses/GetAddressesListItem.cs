using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Api.ApiResponses
{
    public class GetAddressesListItem
    {
        public string Uprn { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Match { get; set; }

        public static implicit operator GetAddressesListItem(SuggestedAddress source)
        {
            return new GetAddressesListItem
            {
                Uprn = source.Uprn,
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                Town = source.Town,
                Postcode = source.Postcode,
                Longitude = source.Longitude,
                Latitude = source.Latitude,
                Match = source.Match
            };
        }
    }
}
