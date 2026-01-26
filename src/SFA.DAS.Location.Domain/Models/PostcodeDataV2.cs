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

    public static PostcodeDataV2 From(SuggestedAddress address)
    {
        if (address is null)
        {
            return null;
        }
        
        string outcode = null;
        string incode = null;
        var postcode = address.Postcode?.Replace(" ", string.Empty);
        if (postcode is { Length: >= 5 })
        {
            outcode = postcode[..^3];
            incode = postcode[^3..];
        }
        
        return new PostcodeDataV2
        {
            Postcode = address.Postcode,
            Outcode = outcode,
            Incode = incode,
            Latitude = address.Latitude,
            Longitude = address.Longitude,
            AdminDistrict = address.LocalCustodian,
            Country = address.Country
        };
    }
}