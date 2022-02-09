using System;
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
        private readonly IPostcodeOutcodeRepository _outcodeRepository;

        public PostcodeService(IPostcodeApiService postcodeApiService, IPostcodeOutcodeRepository outcodeRepository)
        {
            _postcodeApiService = postcodeApiService;
            _outcodeRepository = outcodeRepository ?? throw new ArgumentNullException(nameof(outcodeRepository));
        }

        public async Task<SuggestedLocation> GetDistrictNameByOutcodeQuery(string query)
        {
            var result = await _postcodeApiService.GetDistrictData(query);
            return result;
        }

        public async Task<PostcodeData> GetPostcodeByFullPostcode(string query)
        {
            var result = await _postcodeApiService.GetPostcodeData(query);
            await EnrichPostcodeDataWithOutcodeArea(result);
            return result;
        }

        private async Task EnrichPostcodeDataWithOutcodeArea(PostcodeData postcodeData)
        {
            var outcode = await _outcodeRepository.GetOutcode(postcodeData.Outcode);
            if (outcode != null)
            {
                postcodeData.PostalTown = outcode.PostalTown;
                postcodeData.AreaName = outcode.AreaName;
            }
        }

        public async Task<PostcodeData> GetPostcodeDataByOutcode(string query)
        {
            var result = await _postcodeApiService.GetFullPostcodeDataByOutcode(query);
            await EnrichPostcodeDataWithOutcodeArea(result);
            return result;
        }

        public async Task<IEnumerable<SuggestedLocation>> GetPostcodesByOutcodeQuery(string query, int resultCount = 20)
        {
            var result = await _postcodeApiService.GetAllStartingWithOutcode(query, resultCount);
            return result;
        }
    }
}
