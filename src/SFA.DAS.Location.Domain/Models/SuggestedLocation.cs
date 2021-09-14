using System.Linq;
using SFA.DAS.Location.Domain.ImportTypes;

namespace SFA.DAS.Location.Domain.Models
{
    public class SuggestedLocation
    {
        public string CountyName { get; set; }
        public string LocationName { get; set; }
        public string LocalAuthorityName { get; set; }
        public string Postcode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string AdminDistrict { get; set; }
        public string Outcode { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string LocalAuthorityDistrict { get; set; }


        public static implicit operator SuggestedLocation(PostcodesLocationApiItem source)
        {
            return new SuggestedLocation
            {
                Lat = source.Lat,
                Long = source.Long,
                Postcode = source.Postcode,
                AdminDistrict = "",
                Outcode =source.Outcode,
                Country = source.Country,
                Region = source.Region,
                LocalAuthorityDistrict = source.AdminDistrict
            };
        }

        public static implicit operator SuggestedLocation(Domain.Entities.Location source)
        {
            return new SuggestedLocation
            {
                Lat = source.Lat,
                Long = source.Long,
                CountyName = source.CountyName,
                LocationName = source.LocationName,
                LocalAuthorityName = source.LocalAuthorityName,
                Region = source.Region,
                LocalAuthorityDistrict = source.LocalAuthorityDistrict
            };
        }

        public static implicit operator SuggestedLocation(PostcodeDistrictLocationApiResponse source)
        {
            return new SuggestedLocation
            {
                Lat = source.Lat,
                Long = source.Long,
                Postcode = source.Postcode,
                AdminDistrict = source.AdminDistrict.FirstOrDefault(),
                Outcode =source.Outcode,
                Country = source.Country.FirstOrDefault()
            };
        }
    }
}
