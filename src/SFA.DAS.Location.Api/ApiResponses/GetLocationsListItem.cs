
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Api.ApiResponses
{
    public class GetLocationsListItem
    {
        public string LocalAuthorityName { get; set; }
        public string CountyName { get; set; }
        public string LocationName { get; set; }
        public string Postcode { get; set; }
        public Geometry Location { get; set; }
        public string DistrictName { get; set; }
        public string Outcode { get; set; }
        public string PostalArea { get; set; }
        public string PostalTown { get; set; }
        public string Region { get; set; }
        public string LocalAuthorityDistrict { get; set; }

        public static implicit operator GetLocationsListItem(Domain.Entities.Location source)
        {
            return new GetLocationsListItem
            {
                LocationName = source.LocationName,
                CountyName = source.CountyName,
                LocalAuthorityName = source.LocalAuthorityName,
                Location = new Geometry
                {
                    Coordinates = new[] { source.Lat, source.Long }
                },
                Region = source.Region,
                LocalAuthorityDistrict = source.LocalAuthorityDistrict
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
                Location = new Geometry
                {
                    Coordinates = new[] { source.Lat, source.Long }
                }
            };
        }

        public static implicit operator GetLocationsListItem(PostcodeData source)
        {
            return new GetLocationsListItem
            {
                Postcode = source.Postcode,
                DistrictName = source.AdminDistrict,
                Location = new Geometry
                {
                    Coordinates = new[] { source.Lat, source.Long }
                },
                Outcode = source.Outcode,
                PostalTown = source.PostalTown,
                PostalArea = source.AreaName,
                Region = source.Region,
                LocalAuthorityDistrict = source.AdminDistrict,
            };
        }

        public class Geometry
        {
            public static string Type => "Point";
            public double[] Coordinates { get; set; }
        }
    }
}