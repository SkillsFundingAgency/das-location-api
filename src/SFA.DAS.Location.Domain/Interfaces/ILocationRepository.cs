using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface ILocationRepository
    {
        Task InsertMany(IEnumerable<Domain.Entities.Location> apprenticeshipFundingImports);
        void DeleteAll();
        Task<IEnumerable<Domain.Entities.Location>> GetAllStartingWith(string query, int resultCount = 10);
    }
}