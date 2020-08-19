using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.Location.Data.UnitTests.Repository.Location
{
    public class WhenGettingAll
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
        public async Task Then_The_Locations_Are_Returned()
        {
            //Act
            var actual = await _locationRepository.GetAll();
        
            //Assert
            actual.Should().BeEquivalentTo(_items);
        }     
    }
}