using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Domain.Entities.Location>> GetLocationsByQuery(string query, int resultCount);
    }
}