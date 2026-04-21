using System;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using SFA.DAS.Location.Contracts.ApiRequests;
using SFA.DAS.Location.Contracts.ApiResponses;
using SFA.DAS.Location.Contracts.ApiResponses.V2;
using SFA.DAS.Location.Contracts.Client;
using Geometry = SFA.DAS.Location.Contracts.ApiResponses.Geometry;
using GetLocationsListItem = SFA.DAS.Location.Contracts.ApiResponses.GetLocationsListItem;
using GetLocationsListResponse = SFA.DAS.Location.Contracts.ApiResponses.GetLocationsListResponse;
using GetPostcodesApiRequest = SFA.DAS.Location.Contracts.ApiRequests.V2.GetPostcodesApiRequest;

namespace SFA.DAS.Location.Contracts;

public interface ILocationLookupService
{
    Task<LocationItem> GetLocationInformation(string location, double lat,
        double lon, bool includeDistrictNameInPostcodeDisplayName = false);
    
    Task<GetAddressesListResponse> GetExactMatchAddresses(string fullPostcode);
    Task<PostcodeInfo?> GetPostcodeInfoAsync(string postcode);
}

public class LocationLookupService(ILocationApiClient<LocationApiConfiguration> locationApiClient)
    : ILocationLookupService
{
    private const string PostcodeRegex = @"^[A-Za-z]{1,2}\d[A-Za-z\d]?\s*\d[A-Za-z]{2}$";
    private const string OutcodeRegex = @"^[A-Za-z]{1,2}\d[A-Za-z\d]?";
    private const string OutcodeDistrictRegex = @"^[A-Za-z]{1,2}\d[A-Za-z\d]?\s[A-Za-z]*";
    private const double MinMatch = 1;

    private static readonly TimeSpan RegexTimeOut = TimeSpan.FromMilliseconds(500);

    public async Task<LocationItem> GetLocationInformation(string location, double lat, double lon, bool includeDistrictNameInPostcodeDisplayName = false)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            return null;
        }

        if (lat != 0 && lon != 0)
        {
            return new LocationItem(location, [lat, lon], string.Empty);
        }

        GetLocationsListItem getLocationsListItem  = null;
        
        if (Regex.IsMatch(location, PostcodeRegex, RegexOptions.None, RegexTimeOut))
        { 
            var result = await locationApiClient.Get<LookupPostcodeV2Response>(new GetPostcodesApiRequest((location)));
            getLocationsListItem = result?.ToGetLocationsListItem() ?? new GetLocationsListItem();
            location = getLocationsListItem.GetDisplayName(includeDistrictNameInPostcodeDisplayName);
        }
        else if (Regex.IsMatch(location, OutcodeDistrictRegex, RegexOptions.None, RegexTimeOut))
        {
            getLocationsListItem = await locationApiClient.Get<GetLocationsListItem>(new GetPostcodesOutcodeApiRequest(location.Split(' ').FirstOrDefault()));
            if (getLocationsListItem.Location != null)
            {
                location = getLocationsListItem.GetDisplayName(includeDistrictNameInPostcodeDisplayName);
            }
        }
        else if(Regex.IsMatch(location, OutcodeRegex, RegexOptions.None, RegexTimeOut))
        {
            getLocationsListItem = await locationApiClient.Get<GetLocationsListItem>(new GetPostcodesOutcodeApiRequest(location));
        }
        else if (location.Split(",").Length >= 2)
        {
            
            var locationInformation = location.Split(",");
            var locationName = UrlEncoder.Default.Encode(string.Join(",",locationInformation.Take(locationInformation.Length-1)).Trim());
            var authorityName = locationInformation.Last().Trim();
            getLocationsListItem = await locationApiClient.Get<GetLocationsListItem>(new GetLocationsApiRequest(locationName, authorityName));
        }
        
        if (location.Length >= 2 && getLocationsListItem?.Location == null)
        {
            var locations = await locationApiClient.Get<GetLocationsListResponse>(new GetSearchApiRequest(location,20));

            var locationsListItem = locations?.Locations?.FirstOrDefault();
            if (locationsListItem != null)
            {
                getLocationsListItem = locationsListItem;
                location = getLocationsListItem.GetDisplayName(includeDistrictNameInPostcodeDisplayName);    
            }
        }

        return getLocationsListItem?.Location != null
            ? new LocationItem(location, getLocationsListItem.Location.Coordinates!.ToArray(), getLocationsListItem.Country)
            : null;
    }

    public async Task<GetAddressesListResponse> GetExactMatchAddresses(string fullPostcode)
    {
        if (!Regex.IsMatch(fullPostcode, PostcodeRegex, RegexOptions.None, RegexTimeOut))
        {
            return null;
        }

        var response = await locationApiClient.GetWithResponseCode<GetAddressesListResponse>(new GetAddressesApiRequest(fullPostcode, MinMatch));

        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new InvalidOperationException($"Location api did not return a successful response when trying to get addresses for postcode {fullPostcode}");
        }

        return response.Body;
    }

    public async Task<PostcodeInfo?> GetPostcodeInfoAsync(string postcode)
    {
        var response = await locationApiClient.GetWithResponseCode<LookupPostcodeV2Response>(new GetPostcodesApiRequest(postcode));
        return response.StatusCode == HttpStatusCode.NotFound
            ? null
            : PostcodeInfo.From(response.Body);
    }
}

public record LocationItem(string Name, double[] GeoPoint, string Country)
{
    public decimal? Latitude
    {
        get
        {
            if (GeoPoint?.Length > 0 && decimal.TryParse(GeoPoint[0].ToString(), out var latitude))
            {
                return latitude;
            }
            return null;
        }
    }

    public decimal? Longitude
    {
        get
        {
            if (GeoPoint?.Length > 1 && decimal.TryParse(GeoPoint[1].ToString(), out var longitude))
            {
                return longitude;
            }
            return null;
        }
    }
}

public class PostcodeInfo
{
    public string AdminDistrict { get; set; }
    public string Country { get; set; }
    public string Incode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Outcode { get; set; }
    public string Postcode { get; set; }

    public static PostcodeInfo From(LookupPostcodeV2Response source)
    {
        return new PostcodeInfo
        {
            AdminDistrict = source.DistrictName,
            Country = source.Country,
            Incode = source.Incode,
            Latitude = source.Latitude,
            Longitude = source.Longitude,
            Outcode = source.Outcode,
            Postcode = source.Postcode,
        };
    }
}

public static class LookupPostcodeV2ResponseHelper
{
    public static GetLocationsListItem ToGetLocationsListItem(this LookupPostcodeV2Response source)
    {
        var result = new GetLocationsListItem
        {
            Postcode = source.Postcode,
            Country = source.Country,
            DistrictName = source.DistrictName,
        };

        if (source.Latitude is not null && source.Longitude is not null)
        {
            result.Location = new Geometry
            {
                Coordinates = [source.Latitude.Value, source.Longitude.Value]
            };
        }

        return result;
    }

    public static string GetDisplayName(this GetLocationsListItem source, bool includeDistrictNameInPostcodeDisplayName = false)
    {
        return (!string.IsNullOrEmpty(source.Outcode) && !string.IsNullOrEmpty(source.DistrictName)) ? $"{source.Outcode} {source.DistrictName}" :
            string.IsNullOrEmpty(source.Postcode) ? $"{source.LocationName}, {source.LocalAuthorityName}" : GetPostcodeDisplayName(source, includeDistrictNameInPostcodeDisplayName);
    }
    private static string GetPostcodeDisplayName(GetLocationsListItem source, bool includeDistrictNameInPostcodeDisplayName)
    {
        if (includeDistrictNameInPostcodeDisplayName)
        {
            return $"{source.Postcode}, {source.DistrictName}";
        }
        return $"{source.Postcode}";
    }
}