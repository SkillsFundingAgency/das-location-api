
namespace SFA.DAS.Location.Api.ApiResponses
{
    public class GetLocationsListItem
    {
        public string LocalAuthorityName { get ; set ; }
        public string CountyName { get ; set ; }
        public string LocationName { get ; set ; }
        public string Postcode { get; set; }
        public Geometry Location { get; set; }
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

        public static implicit operator GetLocationsListItem(Domain.Models.SuggestedLocation source)
        {
            return new GetLocationsListItem
            {
                LocationName = source.LocationName,
                CountyName = source.CountyName,
                LocalAuthorityName = source.LocalAuthorityName,
                Postcode = source.Postcode,
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