using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.Location.Data.UnitTests.Repository.Location
{
    public class WhenDeletingAll
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
                    Id = 1
                }
            };
            
            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.Locations).ReturnsDbSet(_items);
            

            _locationRepository = new Data.Repository.LocationRepository(_locationDataContext.Object);
        }

        [Test]
        public void Then_The_Locations_Are_Removed()
        {
            //Act
            _locationRepository.DeleteAll();
            
            //Assert
            _locationDataContext.Verify(x=>x.Locations.RemoveRange(_locationDataContext.Object.Locations), Times.Once);
            _locationDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }       
    }
}