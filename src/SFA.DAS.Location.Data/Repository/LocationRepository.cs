using System.Collections.Generic;
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

        public async Task<IEnumerable<Domain.Entities.Location>> GetAll()
        {
            var results = await _dataContext.Locations.ToListAsync();
            return results;
        }       
    }
}