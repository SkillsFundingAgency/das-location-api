using Newtonsoft.Json;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Infrastructure.ApiClient
{
    public class PostcodeApiService : IPostcodeApiService
    {
        private readonly HttpClient _client;
        public PostcodeApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<SuggestedLocation>> GetAllStartingWithOutcode(string query, int resultCount = 10)
        {
            var items = new List<PostcodesLocationApiItem>();
            var response = await _client.GetAsync(new Uri(string.Format(Constants.PostcodesUrl, query, resultCount)));

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<PostcodesLocationApiResponse>(jsonResponse);
            items.AddRange(item.Result);

            return items.Select(c => (SuggestedLocation)c);
        }

        public async Task<SuggestedLocation> GetPostcodeData(string query)
        {
            var response = await _client.GetAsync(new Uri(string.Format(Constants.PostcodesUrl, query, 1)));

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<PostcodesLocationApiResponse>(jsonResponse);
            var result = item.Result[0];

            return (SuggestedLocation)result;
        }
    }
}
