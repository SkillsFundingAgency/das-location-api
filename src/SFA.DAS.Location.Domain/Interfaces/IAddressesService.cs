using SFA.DAS.Location.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface IAddressesService
    {
        Task<IEnumerable<SuggestedAddress>> FindFromDpaDataset(string query, double minMatch);
    }
}