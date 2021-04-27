using SFA.DAS.Location.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface IPostcodeService
    {
        Task<IEnumerable<SuggestedLocation>> GetPostcodesByOutcodeQuery(string query, int resultCount = 20);
        Task<PostcodeData> GetPostcodeByFullPostcode(string query);
        Task<SuggestedLocation> GetDistrictNameByOutcodeQuery(string query);
        Task<PostcodeData> GetPostcodeDataByOutcode(string query);
    }
}
