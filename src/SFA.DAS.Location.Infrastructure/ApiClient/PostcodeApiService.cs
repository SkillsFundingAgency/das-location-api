using Newtonsoft.Json;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<PostcodesLocationApiResponse>(jsonResponse);

                if (item.Result != null)
                {
                    items.AddRange(item.Result);
                }

                return items.Where(c => c.Country == "England").Select(c => (SuggestedLocation)c);
            }
            else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return null;
            }
            else
            {
                throw new HttpRequestException();
            }
        }

        public async Task<SuggestedLocation> GetDistrictData(string query)
        {

            var response = await _client.GetAsync(new Uri(string.Format(Constants.DistrictNameUrl, query)));
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<PostcodeLocationApiResponse>(jsonResponse);
                var result = (SuggestedLocation)item.Result;
                
                if (result.Country == "England")
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return null;
            }
            else
            {
                throw new HttpRequestException();
            }
        }

        public async Task<PostcodeData> GetPostcodeData(string query)
        {
            var response = await _client.GetAsync(new Uri(string.Format(Constants.PostcodeUrl, query)));

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<PostcodeLocationApiResponse>(jsonResponse);
                var result = item.Result;

                if (result.Country == "England")
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return null;
            }
            else
            {
                throw new HttpRequestException();
            }
        }
    }
}
