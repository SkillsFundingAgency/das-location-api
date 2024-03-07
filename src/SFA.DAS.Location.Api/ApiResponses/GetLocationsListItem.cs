
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Api.ApiResponses
{
    public class GetLocationsListItem
    {
        public string LocalAuthorityName { get ; set ; }
        public string CountyName { get ; set ; }
        public string LocationName { get ; set ; }
        public string Postcode { get; set; }
        public Geometry Location { get; set; }
        public string DistrictName { get; set; }
        public string Outcode { get; set; }
        public string Country { get ; set ; }

        public static implicit operator GetLocationsListItem(Domain.Entities.Location source)
        {
            return new GetLocationsListItem
            {
                LocationName = source.LocationName,
                CountyName = source.CountyName,
                LocalAuthorityName = source.LocalAuthorityName,
                Location = new Geometry
                {
                    Coordinates = new []{ source.Lat, source.Long }
                }
            };
        }

        public static implicit operator GetLocationsListItem(SuggestedLocation source)
        {
            return new GetLocationsListItem
            {
                LocationName = source.LocationName,
                CountyName = source.CountyName,
                LocalAuthorityName = source.LocalAuthorityName,
                Postcode = source.Postcode,
                Outcode = source.Outcode,
                DistrictName = source.AdminDistrict,
                Country = source.Country,
                Location = new Geometry
                {
                    Coordinates = new[] { source.Lat, source.Long }
                }
            };
        }

        public static implicit operator GetLocationsListItem(PostcodeData source)
        {
            if (source == null)
            {
                return null;
                
            }
            return new GetLocationsListItem
            {
                Postcode = source.Postcode,
                DistrictName = source.AdminDistrict,
                Country = source.Country,
                Location = new Geometry
                {
                    Coordinates = new[] { source.Lat, source.Long }
                }
            };
        }

        public class Geometry
        {
            public static string Type => "Point";
            public double[] Coordinates { get; set; }
        }
    }
}