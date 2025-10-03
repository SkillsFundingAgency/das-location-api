using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Api.ApiResponses;

public class LookupPostcodeV2Response
{
    public string Postcode { get; set; }
    public string Outcode { get; set; }
    public string Incode { get; set; }
    public string DistrictName { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Country { get ; set ; }

    public static LookupPostcodeV2Response From(PostcodeDataV2 source)
    {
        return new LookupPostcodeV2Response
        {
            Postcode = source.Postcode,
            DistrictName = source.AdminDistrict,
            Incode = source.Incode,
            Outcode = source.Outcode,
            Country = source.Country,
            Latitude = source.Latitude,
            Longitude = source.Longitude,
        };
    }
}