using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.LocationImport.Services;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Application.UnitTests.LocationImport.Services
{
    public class WhenImportingLocations
    {
        [Test, MoqAutoData]
        public async Task Then_Run_Data_Load_Is_Called(
            [Frozen] Mock<ILocationImportRepository> importRepository,
            LocationImportService importService)
        {
            await importService.Import();

            importRepository.Verify(x => x.RunDataLoad(), Times.Once());
        }
    }
}