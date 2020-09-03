using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.Location.Data.UnitTests.Repository.Location
{
    public class WhenInsertingMany
    {
        private Mock<ILocationDataContext> _locationDataContext;
        private List<Domain.Entities.Location> _items;
        private Data.Repository.LocationRepository _locationRepository;

        [SetUp]
        public void Arrange()
        {
            _items = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location
                {
                    Id = 1
                },
                new Domain.Entities.Location
                {
                    Id = 2
                }
            };
        
            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.Locations).ReturnsDbSet(new List<Domain.Entities.Location>());
        

            _locationRepository = new Data.Repository.LocationRepository(_locationDataContext.Object);
        }
        
        [Test]
        public async Task Then_The_Items_Are_Added()
        {
            //Act
            await _locationRepository.InsertMany(_items);
            
            //Assert
            _locationDataContext.Verify(x=>x.Locations.AddRangeAsync(_items, It.IsAny<CancellationToken>()), Times.Once);
            _locationDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}