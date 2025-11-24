using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface IOsPlacesApiService
    {
        Task<IEnumerable<SuggestedAddress>> FindFromDpaDataset(string query, double minMatch);
        Task<SuggestedPlace> NearestFromDpaDataset(string query, int radius = 50);
    }
}