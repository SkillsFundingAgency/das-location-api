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
            var moreData = true;
            var items = new List<LocationApiItem>();
            var offSet = 0;
            var recordSize = 2000;
            
            while (moreData)
            {
                var response = await _client.GetAsync(new Uri(string.Format(Constants.NationalOfficeOfStatisticsLocationUrl,recordSize,offSet)));
            
                response.EnsureSuccessStatusCode();
            
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<OnsLocationApiResponse>(jsonResponse);

                moreData = item.ExceededTransferLimit;

                offSet += recordSize;
                
                items.AddRange(item.Features);
            }

            return items;
        }
    }
}