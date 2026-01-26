using Newtonsoft.Json;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Infrastructure.ApiClient;

public class OsPlacesApiService(HttpClient client, LocationApiConfiguration config) : IOsPlacesApiService
{
    public async Task<IEnumerable<SuggestedAddress>> FindFromDpaDataset(string query, double minMatch)
    {
        if (minMatch is < 0.1 or > 1.0)
            throw new ArgumentOutOfRangeException($"{nameof(minMatch)} must be between 0.1 and 1.0", nameof(minMatch));

        var items = new List<DpaResultPlacesApiItem>();
        client.DefaultRequestHeaders.Add("key", config.OsPlacesApiKey);
        var response = await client.GetAsync(new Uri(string.Format(Constants.OsPlacesFindUrl, query, "dpa", Math.Round(minMatch, 1, MidpointRounding.ToZero), DecimalPlaces(minMatch, 10))));

        if (response.StatusCode.Equals(HttpStatusCode.NotFound))
        {
            return new List<SuggestedAddress>();
        }

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var item = JsonConvert.DeserializeObject<OsPlacesApiResponse>(jsonResponse);

        if (item.Results != null)
        {
            items.AddRange(item.Results.Select(p => p.Dpa));
        }

        // Filter + sort
        return SortAndFilterSuggestedAddresses(items, minMatch);
    }

    public async Task<IEnumerable<SuggestedAddress>> FindFromDpaOsPlaces(string query,
        double minMatch = 1.0,
        CancellationToken cancellation = default)
    {
        return await FindFromDpaOsPlaces(query, null, minMatch, cancellation);
    }
    
    public async Task<IEnumerable<SuggestedAddress>> FindFromDpaOsPlaces(
        string query,
        int? maxResults = null,
        double minMatch = 1.0,
        CancellationToken cancellation = default)
    {
        if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("Query must not be null or empty.", nameof(query));
        if (maxResults is < 1 or > 100) throw new ArgumentOutOfRangeException(nameof(maxResults), maxResults, "maxResults must be between 1 and 100 (inclusive)");
        if (minMatch is < 0.1 or > 1.0) throw new ArgumentOutOfRangeException(nameof(minMatch), minMatch, "minMatch must be between 0.1 and 1.0 (inclusive).");
        
        using var request = new HttpRequestMessage(HttpMethod.Get, OsPlacesUrlBuilder.Create(query, maxResults: maxResults));
        request.Headers.TryAddWithoutValidation("key", config.OsPlacesApiKey);

        using var response = await client.SendAsync(request, cancellation);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return [];

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellation);
        var payload = JsonConvert.DeserializeObject<OsPlacesApiResponse>(jsonResponse);
        if (payload?.Results == null || payload.Results.Count == 0)
            return [];

        var items = payload.Results
            .Select(r => r.Dpa)
            .Where(d => d != null)
            .ToList();

        // Filter + sort
        return SortAndFilterSuggestedAddresses(items, minMatch);
    }

    public async Task<SuggestedPlace> NearestFromDpaDataset(string query, int radius = 50)
    {
        // https://docs.os.uk/os-apis/accessing-os-apis/os-places-api/technical-specification/nearest
        using var request = new HttpRequestMessage(HttpMethod.Get,
            string.Format(Constants.OsPlacesNearestUrl, query, radius, "dpa"));

        request.Headers.Add("key", config.OsPlacesApiKey);

        using var response = await client.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return Empty;

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var api = JsonConvert.DeserializeObject<OsNearestApiResponse>(json);

        var dpa = api?.Results?.FirstOrDefault()?.Dpa;

        return dpa == null ? Empty : SuggestedPlace.From(dpa);
    }

    private class MixedComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            var firstIsNumber = int.TryParse(s1, out int first);
            var secondIsNumber = int.TryParse(s2, out int second);
            if (firstIsNumber && secondIsNumber)
                return first.CompareTo(second);
                
            if (firstIsNumber && !secondIsNumber)
            {
                if (SplitNumber(s2, out second))
                {
                    return first.CompareTo(second);
                }
                else
                {
                    return -1;
                }
            }

            if (!firstIsNumber && secondIsNumber)
            {
                if (SplitNumber(s1, out first))
                {
                    return first.CompareTo(second);
                }
                else
                {
                    return 1;
                }
            }

            if(!firstIsNumber && !secondIsNumber)
            {
                if (SplitNumber(s1, out first) && SplitNumber(s2, out second))
                {
                    return first.CompareTo(second);
                }
            }

            return (s1 ?? string.Empty).CompareTo(s2 ?? string.Empty);
        }

        private bool SplitNumber(string input, out int number)
        {
            var onlyDigits = Regex.Replace(input ?? string.Empty, @"[^\d]", "");
            return int.TryParse(onlyDigits, out number);
        }
    }

    private static int DecimalPlaces(double input, int maxDecimalPlaces)
    {
        var inputAsString = string.Format("{0:0." + new string('0', maxDecimalPlaces - 1) + "#}", input).TrimEnd('0');
        var decimalPlaces = Math.Max(1, inputAsString.Length - (inputAsString.IndexOf('.') + 1));
            
        return decimalPlaces;
    }

    public static readonly SuggestedPlace Empty = new();

    private static List<SuggestedAddress> SortAndFilterSuggestedAddresses(List<DpaResultPlacesApiItem> items, double minMatch)
    {
        return items
            .Where(c => c.Match >= minMatch) // only include postal addresses and match using full match precision
            .OrderBy(c => c.MatchDescription == "EXACT" ? 0 : 1) // sort the exact matches first
            .ThenByDescending(c => c.Match) // then sort by the match score highest first
            .ThenBy(c => $"{c.SubBuildingName}", new MixedComparer()) // then sort lowest first by the number part of a sub building name e.g. Flat 10
            .ThenBy(c => $"{c.BuildingNumber}", new MixedComparer()) // and finally sort lowest first by the building name
            .Select(SuggestedAddress.From)
            .ToList();
    }
}