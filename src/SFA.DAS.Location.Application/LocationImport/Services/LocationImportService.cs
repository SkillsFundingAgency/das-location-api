using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.Location.Domain.Entities;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.LocationImport.Services
{
    public class LocationImportService : ILocationImportService
    {
        private readonly INationalStatisticsLocationService _nationalStatisticsLocationService;
        private readonly ILocationImportRepository _importRepository;
        private readonly ILocationRepository _repository;
        private readonly IImportAuditRepository _auditRepository;
        private readonly ILogger<LocationImportService> _logger;

        public LocationImportService (
            INationalStatisticsLocationService nationalStatisticsLocationService, 
            ILocationImportRepository importRepository,
            ILocationRepository repository, 
            IImportAuditRepository auditRepository, 
            ILogger<LocationImportService> logger)
        {
            _nationalStatisticsLocationService = nationalStatisticsLocationService;
            _importRepository = importRepository;
            _repository = repository;
            _auditRepository = auditRepository;
            _logger = logger;
        }
        public async Task Import()
        {
            _logger.LogInformation("Import starting");
            var timeStarted = DateTime.UtcNow;
            
            var items = (await _nationalStatisticsLocationService.GetLocations()).ToList();

            if (!items.Any())
            {
                _logger.LogWarning("No items to import");
                return;
            }
            
            _logger.LogInformation("Deleting from Import table");
            _importRepository.DeleteAll();
            _logger.LogInformation("Inserting into Import table");
            
            var featureItems = items
                .GroupBy(c => new {c.Attributes.Id})
                .Select(item => item.First())
                .Where(item=>!string.IsNullOrEmpty(item.Attributes.CountyName))
                .GroupBy(c=>new {c.Attributes.CountyName, c.Attributes.LocationName})
                .Select(item => item.First())
                .ToList();
            
            await _importRepository.InsertMany(featureItems.Select(c => (Domain.Entities.LocationImport) c.Attributes).ToList());
            
            var importedItems = (await _importRepository.GetAll()).ToList();

            _logger.LogInformation("Deleting from main table");
            _repository.DeleteAll();
            
            _logger.LogInformation("Inserting into main table");
            await _repository.InsertMany(importedItems.Select(c => (Domain.Entities.Location) c).ToList());

            await _auditRepository.Insert(new ImportAudit(timeStarted, importedItems.Count));
        }
    }
}