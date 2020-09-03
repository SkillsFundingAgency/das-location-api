using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.Location.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;

        public LocationService (ILocationRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Domain.Entities.Location>> GetLocationsByQuery(string query, int resultCount)
        {
            var results = await _repository.GetAllStartingWith(query, resultCount);

            return results;
        }

        public async Task<Domain.Entities.Location> GetLocationsByLocationAuthorityName(string locationName, string authorityName)
        {
            var result = await _repository.GetByLocationAndAuthorityName(locationName, authorityName);

            return result;
        }
    }
}