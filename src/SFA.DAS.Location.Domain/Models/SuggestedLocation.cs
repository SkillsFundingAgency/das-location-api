using SFA.DAS.Location.Domain.ImportTypes;
using System;
using System.Collections.Generic;
using System.Text;

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

        public static implicit operator SuggestedLocation(PostcodesLocationApiResponse source)
        {
            return new SuggestedLocation
            {
                Lat = source.Lat,
                Long = source.Long,
                Postcode = source.Postcode
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
            };
        }
    }

}
