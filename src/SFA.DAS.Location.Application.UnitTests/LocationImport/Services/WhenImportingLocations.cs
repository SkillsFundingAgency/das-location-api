using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.LocationImport.Services;
using SFA.DAS.Location.Domain.Entities;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Application.UnitTests.LocationImport.Services
{
    public class WhenImportingLocations
    {
        [Test, MoqAutoData]
        public async Task Then_The_Location_Items_Are_Retrieved_From_The_Api(
            [Frozen]Mock<ILocationService> service,
            LocationImportService importService)
        {
            await importService.Import();
            
            service.Verify(x=>x.GetLocations(), Times.Once);
        }

        [Test, MoqAutoData]
        public async Task Then_If_No_Locations_Are_Returned_From_The_Api_Nothing_Is_Imported(
            [Frozen]Mock<ILocationService> service,
            [Frozen]Mock<ILocationImportRepository> importRepository,
            [Frozen]Mock<ILocationRepository> repository,
            LocationImportService importService)
        {
            service.Setup(x => x.GetLocations()).ReturnsAsync(new List<LocationApiItem>());

            await importService.Import();
            
            importRepository.Verify(x=>x.DeleteAll(), Times.Never);
            repository.Verify(x=>x.DeleteAll(), Times.Never);
        }

        [Test, MoqAutoData]
        public async Task Then_The_Items_Are_Deleted_From_The_ImportRepository_And_Distinct_Location_Items_From_The_Api_Are_Added_To_The_Import_Repository(
            List<LocationApiItem> apiResponse,
            LocationApiItem apiFeature,
            [Frozen]Mock<ILocationService> service,
            [Frozen]Mock<ILocationImportRepository> importRepository,
            LocationImportService importService)
        {
            apiResponse.Add(apiFeature);
            apiResponse.Add(apiFeature);
            service.Setup(x => x.GetLocations()).ReturnsAsync(apiResponse);
            
            await importService.Import();

            importRepository.Verify(x=>x.DeleteAll(), Times.Once);
            importRepository.Verify(
                x => x.InsertMany(
                    It.Is<List<Domain.Entities.LocationImport>>(
                        c => c.Count.Equals(apiResponse.Count-1))), Times.Once);
        }

        
        [Test, MoqAutoData]
        public async Task Then_The_Items_Are_Deleted_From_The_ImportRepository_And_Location_Items_With_Distinct_Name_And_Location_From_The_Api_Are_Added_To_The_Import_Repository(
            List<LocationApiItem> apiResponse,
            LocationApiItem apiFeature,
            LocationApiItem apiFeature1,
            [Frozen]Mock<ILocationService> service,
            [Frozen]Mock<ILocationImportRepository> importRepository,
            LocationImportService importService)
        {
            apiFeature1.Attributes.CountyName = apiFeature.Attributes.CountyName;
            apiFeature1.Attributes.LocationName = apiFeature.Attributes.LocationName;
                
            apiResponse.Add(apiFeature);
            apiResponse.Add(apiFeature1);
            service.Setup(x => x.GetLocations()).ReturnsAsync(apiResponse);
            
            await importService.Import();

            importRepository.Verify(x=>x.DeleteAll(), Times.Once);
            importRepository.Verify(
                x => x.InsertMany(
                    It.Is<List<Domain.Entities.LocationImport>>(
                        c => c.Count.Equals(apiResponse.Count-1))), Times.Once);
        }
        
        
        [Test, MoqAutoData]
        public async Task Then_The_Items_Are_Deleted_From_The_ImportRepository_And_Location_Items_With_A_CountName_Are_Added_To_The_Import_Repository(
            List<LocationApiItem> apiResponse,
            LocationApiItem apiFeature,
            LocationApiItem apiFeature1,
            [Frozen]Mock<ILocationService> service,
            [Frozen]Mock<ILocationImportRepository> importRepository,
            LocationImportService importService)
        {
            apiFeature.Attributes.CountyName = null;
            apiFeature1.Attributes.CountyName = "";
            
            apiResponse.Add(apiFeature);
            apiResponse.Add(apiFeature1);
            service.Setup(x => x.GetLocations()).ReturnsAsync(apiResponse);
            
            await importService.Import();

            importRepository.Verify(x=>x.DeleteAll(), Times.Once);
            importRepository.Verify(
                x => x.InsertMany(
                    It.Is<List<Domain.Entities.LocationImport>>(
                        c => c.Count.Equals(apiResponse.Count-2))), Times.Once);
        }
        
        [Test, MoqAutoData]
        public async Task Then_The_Items_Are_Deleted_From_The_Repository_And_The_LocationImport_Items_Are_Added_To_The_Location_Repository(
            List<LocationApiItem> apiResponse,
            List<Domain.Entities.LocationImport> importItems,
            [Frozen]Mock<ILocationService> service,
            [Frozen]Mock<ILocationImportRepository> importRepository,
            [Frozen]Mock<ILocationRepository> repository,
            LocationImportService importService)
        {
            service.Setup(x => x.GetLocations()).ReturnsAsync(apiResponse);
            importRepository.Setup(x => x.GetAll()).ReturnsAsync(importItems);
            
            await importService.Import();

            repository.Verify(x=>x.DeleteAll(), Times.Once);
            repository.Verify(
                x => x.InsertMany(
                    It.Is<List<Domain.Entities.Location>>(
                        c => c.Count.Equals(importItems.Count))), Times.Once);
        }

        [Test, MoqAutoData]
        public async Task Then_An_Audit_Record_Is_Created(
            List<LocationApiItem> apiResponse,
            List<Domain.Entities.LocationImport> importItems,
            [Frozen]Mock<ILocationService> service,
            [Frozen]Mock<ILocationImportRepository> importRepository,
            [Frozen]Mock<ILocationRepository> repository,
            [Frozen]Mock<IImportAuditRepository> auditRepository,
            LocationImportService importService)
        {
            service.Setup(x => x.GetLocations()).ReturnsAsync(apiResponse);
            importRepository.Setup(x => x.GetAll()).ReturnsAsync(importItems);
            
            await importService.Import();
            
            auditRepository.Verify(x=>x.Insert(It.Is<ImportAudit>(c=>c.RowsImported.Equals(importItems.Count))));
        }
    }
}