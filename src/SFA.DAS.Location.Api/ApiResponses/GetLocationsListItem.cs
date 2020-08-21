namespace SFA.DAS.Location.Api.ApiResponses
{
    public class GetLocationsListItem
    {
        public string LocalAuthorityName { get ; set ; }

        public string CountyName { get ; set ; }

        public double Long { get ; set ; }

        public double Lat { get ; set ; }

        public string LocationName { get ; set ; }

        public static implicit operator GetLocationsListItem(Domain.Entities.Location source)
        {
            return new GetLocationsListItem
            {
                LocationName = source.LocationName,
                Lat = source.Lat,
                Long = source.Long,
                CountyName = source.CountyName,
                LocalAuthorityName = source.LocalAuthorityName
            };
        }
    }
}