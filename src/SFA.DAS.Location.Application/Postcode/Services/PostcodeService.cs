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

        public Task<AdminDistrictData> GetDistrictNameByOutcodeQuery(string query)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PostcodeData> GetPostcodeByFullPostcode(string query)
        {
            var result = await _postcodeApiService.GetPostcodeData(query);
            return result;
        }

        public async Task<IEnumerable<SuggestedLocation>> GetPostcodeByOutcodeQuery(string query, int resultCount)
        {
            var result = await _postcodeApiService.GetAllStartingWithOutcode(query, resultCount);
            return result;
        }
    }
}
