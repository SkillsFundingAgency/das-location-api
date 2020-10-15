using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface IPostcodeService
    {
        Task<IEnumerable<SuggestedLocation>> GetPostcodeByOutcodeQuery(string query, int resultCount);
        Task<PostcodeData> GetPostcodeByFullPostcode(string query);
        Task<AdminDistrictData> GetDistrictNameByOutcodeQuery(string query);
    }
}
