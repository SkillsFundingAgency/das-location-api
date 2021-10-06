using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;

namespace SFA.DAS.Location.Application.Addresses.Services
{
    public class AddressesService : IAddressesService
    {
        private readonly IOsPlacesApiService _service;

        public AddressesService(IOsPlacesApiService service)
        {
            _service = service;
        }
        
        public async Task<IEnumerable<SuggestedAddress>> FindFromLpiDataset(string query, double minMatch)
        {
            var results = await _service.FindFromLpiDataset(query, minMatch);

            return results;
        }
    }
}