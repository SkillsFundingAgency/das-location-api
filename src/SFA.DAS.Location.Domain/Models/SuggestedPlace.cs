using SFA.DAS.Location.Domain.Extensions;
using SFA.DAS.Location.Domain.ImportTypes;

namespace SFA.DAS.Location.Domain.Models;
public record SuggestedPlace
{
    public string Uprn { get; init; }
    public string BuildingName { get; init; }
    public string AddressLine1 { get; init; }
    public string AddressLine2 { get; init; }
    public string AddressLine3 { get; init; }
    public string Postcode { get; init; }
    public string Country { get; init; }
    public double? Longitude { get; init; }
    public double? Latitude { get; init; }

    public static SuggestedPlace From(OsNearestApiResponse.Dpa nearestResult)
    {
        if (nearestResult is null) return null;

        return new SuggestedPlace
        {
            Uprn = nearestResult.Uprn,
            BuildingName = nearestResult.Organisationname,
            AddressLine1 = nearestResult.Buildingname,
            AddressLine2 = nearestResult.Thoroughfarename,
            AddressLine3 = nearestResult.Posttown,
            Postcode = nearestResult.Postcode,
            Country = nearestResult.Countrycode.ToCountry(),
            Longitude = nearestResult.Lng,
            Latitude = nearestResult.Lat,
        };
    }
}