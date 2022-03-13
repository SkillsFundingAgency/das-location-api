using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.LocationImport.Services
{
    public class LocationImportService : ILocationImportService
    {
        private readonly ILocationImportRepository _importRepository;

        public LocationImportService (ILocationImportRepository importRepository)
        {
            _importRepository = importRepository;
        }
        public async Task Import()
        {
            await _importRepository.RunDataLoad();
        }
    }
}