using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface IPostcodeApiService
    {
        Task<IEnumerable<SuggestedLocation>> GetAllStartingWithOutcode(string query, int resultCount);

        Task<PostcodeData> GetPostcodeData(string query);
        Task<SuggestedLocation> GetDistrictData(string query);

        Task<List<PostcodeData>> GetBulkPostCodeData(GetBulkPostcodeRequest postcodes);
    }
}