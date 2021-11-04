using SFA.DAS.Location.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Infrastructure.ApiClient
{
    public interface IOsPlacesApiService
    {
        Task<IEnumerable<SuggestedAddress>> FindFromDpaDataset(string query, double minMatch);
    }
}