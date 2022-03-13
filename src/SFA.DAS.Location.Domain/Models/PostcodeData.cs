using SFA.DAS.Location.Domain.ImportTypes;

namespace SFA.DAS.Location.Domain.Models
{
    public class PostcodeData
    {
        public string Postcode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string AdminDistrict { get ; set ; }
        public string Country { get; set; }
        public string Region { get; set; }

        public static implicit operator PostcodeData(PostcodesLocationApiItem source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new PostcodeData
            {
                Postcode = source.Postcode,
                Lat = source.Lat,
                Long = source.Long,
                AdminDistrict = source.AdminDistrict,
                Country = source.Country,
                Region = source.Region
            };
        }
    }
}
