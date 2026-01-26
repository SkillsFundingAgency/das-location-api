using SFA.DAS.Location.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface IOsPlacesApiService
    {
        Task<IEnumerable<SuggestedAddress>> FindFromDpaDataset(string query, double minMatch);
        Task<IEnumerable<SuggestedAddress>> FindFromDpaOsPlaces(string query, double minMatch, CancellationToken cancellation = default);
        Task<IEnumerable<SuggestedAddress>> FindFromDpaOsPlaces(string query, int? maxResults = null, double minMatch = 1.0, CancellationToken cancellation = default);
        Task<SuggestedPlace> NearestFromDpaDataset(string query, int radius = 50);
    }
}