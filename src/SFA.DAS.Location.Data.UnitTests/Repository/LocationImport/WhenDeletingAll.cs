using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.Location.Data.UnitTests.Repository.LocationImport
{
    public class WhenDeletingAll
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
                    Id = 1
                }
            };

            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.LocationImports).ReturnsDbSet(_items);


            _locationImportRepository = new Data.Repository.LocationImportRepository(_locationDataContext.Object);
        }

        [Test]
        public void Then_The_LocationImport_Items_Are_Removed()
        {
            //Act
            _locationImportRepository.DeleteAll();

            //Assert
            _locationDataContext.Verify(x => x.LocationImports.RemoveRange(_locationDataContext.Object.LocationImports),
                Times.Once);
            _locationDataContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}