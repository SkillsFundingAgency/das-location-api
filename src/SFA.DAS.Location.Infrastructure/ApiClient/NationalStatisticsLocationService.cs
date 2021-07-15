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
    public class NationalStatisticsLocationService : INationalStatisticsLocationService
    {
        private readonly HttpClient _client;
        private int _offSet = 0;
        private readonly int recordSize = 2000;
        
        public NationalStatisticsLocationService(HttpClient client)
        {
            _client = client;
        }

        public string GetName()
        {
            return string.Format(Constants.NationalOfficeOfStatisticsLocationUrl, recordSize, _offSet);
        }

        public async Task<IEnumerable<LocationApiItem>> GetLocations()
        {
            var moreData = true;
            var items = new List<LocationApiItem>();
            
            
            while (moreData)
            {
                var response = await _client.GetAsync(new Uri(GetName()));
            
                response.EnsureSuccessStatusCode();
            
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<OnsLocationApiResponse>(jsonResponse);

                moreData = item.ExceededTransferLimit;

                _offSet += recordSize;
                
                items.AddRange(item.Features);
            }

            return items;
        }
    }
}