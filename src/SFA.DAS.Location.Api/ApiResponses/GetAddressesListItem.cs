using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Api.ApiResponses
{
    public class GetAddressesListItem
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

        public static implicit operator GetAddressesListItem(SuggestedAddress source)
        {
            return new GetAddressesListItem
            {
                Uprn = source.Uprn,
                Organisation = source.Organisation,
                House = source.House,
                Street = source.Street,
                Locality = source.Locality,
                PostTown = source.PostTown,
                County = source.County,
                Postcode = source.Postcode,
                Longitude = source.Longitude,
                Latitude = source.Latitude,
                Match = source.Match
            };
        }
    }
}
