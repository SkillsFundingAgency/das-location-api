using SFA.DAS.Location.Domain.ImportTypes;

namespace SFA.DAS.Location.Domain.Entities
{
    public class LocationImport : LocationBase
    {
        public static implicit operator LocationImport(Attributes source)
        {
            return new LocationImport
            {
                Id = source.Id,
                CountyName = source.CountyName,
                LocationName = source.LocationName,
                LocalAuthorityName = source.LocalAuthorityName,
                Lat = source.Lat,
                Long = source.Long
            };
        }
    }
}