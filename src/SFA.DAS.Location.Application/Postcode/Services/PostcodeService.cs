using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.Postcode.Services
{
    public class PostcodeService : IPostcodeService
    {
        private readonly IPostcodeApiService _postcodeApiService;

        public PostcodeService(IPostcodeApiService postcodeApiService)
        {
            _postcodeApiService = postcodeApiService;
        }

        public async Task<SuggestedLocation> GetDistrictNameByOutcodeQuery(string query)
        {
            var result = await _postcodeApiService.GetDistrictData(query);
            return result;
        }

        public async Task<PostcodeData> GetPostcodeByFullPostcode(string query)
        {
            var result = await _postcodeApiService.GetPostcodeData(query);
            return result;
        }

        public async Task<IEnumerable<SuggestedLocation>> GetPostcodesByOutcodeQuery(string query, int resultCount = 20)
        {
            var result = await _postcodeApiService.GetAllStartingWithOutcode(query, resultCount);
            return result;
        }
    }
}
