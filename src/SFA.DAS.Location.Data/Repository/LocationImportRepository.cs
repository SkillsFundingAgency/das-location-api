using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.Location.Domain.Entities;

namespace SFA.DAS.Location.Data.Repository
{
    public class LocationImportRepository
    {
        private readonly ILocationDataContext _dataContext;

        public LocationImportRepository (ILocationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeleteAll()
        {
            _dataContext.LocationImports.RemoveRange(_dataContext.LocationImports);
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<LocationImport> items)
        {
            await _dataContext.LocationImports.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<LocationImport>> GetAll()
        {
            var results = await _dataContext.LocationImports.ToListAsync();

            return results;
        }
    }
}