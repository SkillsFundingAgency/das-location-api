﻿using Newtonsoft.Json;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Infrastructure.ApiClient
{
    public class OsPlacesApiService : IOsPlacesApiService
    {
        private readonly HttpClient _client;
        private readonly LocationApiConfiguration _config;

        public OsPlacesApiService(HttpClient client, LocationApiConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IEnumerable<SuggestedAddress>> FindFromLpiDataset(string query, double minMatch)
        {
            if (minMatch < 0.1 || minMatch > 1.0)
                throw new ArgumentOutOfRangeException($"{nameof(minMatch)} must be between 0.1 and 1.0", nameof(minMatch));

            var items = new List<LpiResultPlacesApiItem>();
            var response = await _client.GetAsync(new Uri(string.Format(Constants.OsPlacesFindUrl, _config.OsPlacesApiKey, 
                query, "lpi", Math.Round(minMatch, 1, MidpointRounding.ToZero), DecimalPlaces(minMatch, 10))));

            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return null;
            }
            
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<OsPlacesApiResponse>(jsonResponse);

                if (item.Results != null)
                {
                    items.AddRange(item.Results.Select(p => p.Lpi));
                }

                return items
                    .Where(c => c.PostalAddressCode == "D" && c.Match >= minMatch) // only include postal addresses and match using full match precision
                    .OrderBy(c => (c.MatchDescrption == "EXACT" ? 0 : 1)) // sort the exact matches first
                    .ThenByDescending(c => c.Match) // then sort by the match score highest first
                    .ThenBy(c => c.PaoStartNumber, new MixedComparer()) // then sort by the house number lowest first
                    .Select(c => (SuggestedAddress)c);
            }

            response.EnsureSuccessStatusCode();

            return null;
        }

        public class MixedComparer : IComparer<string>
        {
            public int Compare(string s1, string s2)
            {
                var firstIsNumber = int.TryParse(s1, out int first);
                var secondIsNumber = int.TryParse(s2, out int second);
                if (firstIsNumber && secondIsNumber)
                    return first.CompareTo(second);
                
                if (firstIsNumber && !secondIsNumber)
                {
                    if (this.SplitNumber(s2, out second))
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
                    if (this.SplitNumber(s1, out first))
                    {
                        return second.CompareTo(first);
                    }
                    else
                    {
                        return 1;
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

        private int DecimalPlaces(double input, int maxDecimalPlaces)
        {
            var inputAsString = string.Format("{0:0." + new string('0', maxDecimalPlaces-1) + "#}", input).Trim('0');
            return inputAsString.Length-(inputAsString.IndexOf('.') + 1);
        }
    }
}
