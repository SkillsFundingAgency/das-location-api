using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Infrastructure.ApiClient
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _client;

        public LocationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<LocationApiItem>> GetLocations()
        {
            var response = await _client.GetAsync(new Uri(Constants.NationalOfficeOfStatisticsLocationUrl));
            
            response.EnsureSuccessStatusCode();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<List<LocationApiItem>>(jsonResponse);
        }
    }
}