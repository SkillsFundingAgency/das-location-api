using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace SFA.DAS.Location.Infrastructure.ApiClient;

public static class OsPlacesUrlBuilder
{
    private const string DefaultDataset = "dpa";
    private const string DefaultSpatialReferenceSystem = "EPSG:4326";
    private const string DefaultFilter = "COUNTRY_CODE:E";
    private const string OsPlacesPostCodeBaseUrl = "https://api.os.uk/search/places/v1/postcode";
    
    public static string Create(string query, string dataset = DefaultDataset, string outputSrs = DefaultSpatialReferenceSystem, string filter = DefaultFilter, int? maxResults = null)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["postcode"] = query,
            ["dataset"] = dataset,
            ["output_srs"] = outputSrs,
            ["fq"] = filter,
        };
        
        if (maxResults is not null)
        {
            queryParams.Add("maxresults", $"{maxResults}");
        }
        
        return QueryHelpers.AddQueryString(OsPlacesPostCodeBaseUrl, queryParams);
    }
}