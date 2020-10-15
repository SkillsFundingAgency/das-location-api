using SFA.DAS.Location.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Infrastructure.ApiClient
{
    public interface IPostcodeApiService
    {
        Task<IEnumerable<SuggestedLocation>> GetAllStartingWithOutcode(string query, int resultCount);

        Task<PostcodeData> GetPostcodeData(string query);
        Task<SuggestedLocation> GetDistrictData(string query);
    }
}