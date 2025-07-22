using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Application.Addresses.Services
{
    public class AddressesService(IOsPlacesApiService service) : IAddressesService
    {
        public async Task<IEnumerable<SuggestedAddress>> FindFromDpaDataset(string query, double minMatch)
        {
            var results = await service.FindFromDpaDataset(query, minMatch);

            return results;
        }

        public async Task<SuggestedPlace> NearestFromDpaDataset(string query, int radius = 50)
        {
            return await service.NearestFromDpaDataset(query, radius);
        }
    }
}