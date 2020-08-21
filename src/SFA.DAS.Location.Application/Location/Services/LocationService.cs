using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.Location.Services
{
    public class LocationService
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
    }
}