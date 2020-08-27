using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.Location.Data.UnitTests.Repository.LocationImport
{
    public class WhenInsertingMany
    {
        private Mock<ILocationDataContext> _locationDataContext;
        private List<Domain.Entities.LocationImport> _items;
        private Data.Repository.LocationImportRepository _locationImportRepository;

        [SetUp]
        public void Arrange()
        {
            _items = new List<Domain.Entities.LocationImport>
            {
                new Domain.Entities.LocationImport
                {
                    Id = 1
                },
                new Domain.Entities.LocationImport
                {
                    Id = 2
                }
            };
        
            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.LocationImports).ReturnsDbSet(new List<Domain.Entities.LocationImport>());
        

            _locationImportRepository = new Data.Repository.LocationImportRepository(_locationDataContext.Object);
        }
        
        [Test]
        public async Task Then_The_Items_Are_Added()
        {
            //Act
            await _locationImportRepository.InsertMany(_items);
            
            //Assert
            _locationDataContext.Verify(x=>x.LocationImports.AddRangeAsync(_items, It.IsAny<CancellationToken>()), Times.Once);
            _locationDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}