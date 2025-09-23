using System.Linq;
using SFA.DAS.Location.Domain.ImportTypes;

namespace SFA.DAS.Location.Domain.Models;

public class PostcodeDataV2
{
    public string Postcode { get; set; }
    public string Outcode { get; set; }
    public string Incode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string AdminDistrict { get; set; }
    public string Country { get; set; }

    public static PostcodeDataV2 From(PostcodeLookupResult response)
    {
        return new PostcodeDataV2
        {
            Postcode = response.Postcode,
            Outcode = response.Outcode,
            Incode = response.Incode,
            Latitude = response.Latitude,
            Longitude = response.Longitude,
            AdminDistrict = response.AdminDistrict,
            Country = response.Country
        };
    }
}