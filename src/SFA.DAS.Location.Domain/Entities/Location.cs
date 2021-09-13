namespace SFA.DAS.Location.Domain.Entities
{
    public class Location : LocationBase
    {
        public static implicit operator Location(LocationImport source)
        {
            return new Location
            {
                Id = source.Id,
                Lat = source.Lat,
                Long = source.Long,
                CountyName = source.CountyName,
                LocationName = source.LocationName,
                LocalAuthorityName = source.LocalAuthorityName,
                Region = source.Region,
                LocalAuthorityDistrict = source.LocalAuthorityDistrict
            };
        }
    }
}