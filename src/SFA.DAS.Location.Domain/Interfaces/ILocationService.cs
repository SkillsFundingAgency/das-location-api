using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.ImportTypes;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationApiItem>> GetLocations();
    }
}