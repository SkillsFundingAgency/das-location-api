namespace SFA.DAS.Location.Domain.ImportTypes;

public class PostcodeLookupResponse
{
    public PostcodeLookupResult Result { get; set; }
}

public class PostcodeLookupResult
{
    public string Postcode { get; set; }
    public string Outcode { get; set; }
    public string Incode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string AdminDistrict { get; set; }
    public string Country { get; set; }
}