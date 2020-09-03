using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.LocationImport.Handlers.ImportLocations;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Application.UnitTests.LocationImport.Handlers
{
    public class WhenHandlingTheRequest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Request_Is_Handled_And_The_Import_Locations_Called(
            ImportDataCommand command,
            [Frozen]Mock<ILocationImportService> importService,
            ImportDataCommandHandler handler)
        {
            await handler.Handle(command, It.IsAny<CancellationToken>());
            
            importService.Verify(x=>x.Import(), Times.Once);
        }
    }
}