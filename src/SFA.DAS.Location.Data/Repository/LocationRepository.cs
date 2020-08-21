using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Data.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ILocationDataContext _dataContext;

        public LocationRepository(ILocationDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task InsertMany(IEnumerable<Domain.Entities.Location> apprenticeshipFundingImports)
        {
            await _dataContext.Locations.AddRangeAsync(apprenticeshipFundingImports);
            
            _dataContext.SaveChanges();
        }
        public void DeleteAll()
        {
            _dataContext.Locations.RemoveRange(_dataContext.Locations);
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<Domain.Entities.Location>> GetAllStartingWith(string query, int resultCount = 10)
        {
            var results = await _dataContext
                .Locations
                .Where(c=>c.LocationName.StartsWith(query))
                .Take(resultCount)
                .ToListAsync();
            return results;
        }       
    }
}