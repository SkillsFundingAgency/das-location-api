using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.Location.Data.UnitTests.Repository.Location
{
    public class WhenGettingAllMathcingQuery
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
                    Id = 1,
                    LocationName = "testing"
                },
                new Domain.Entities.Location
                {
                    Id = 1,
                    LocationName = "test"
                },
                new Domain.Entities.Location
                {
                    Id = 1,
                    LocationName = "retest"
                },
                new Domain.Entities.Location
                {
                    Id = 1,
                    LocationName = "different"
                }
                
            };
        
            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.Locations).ReturnsDbSet(_items);
        

            _locationRepository = new Data.Repository.LocationRepository(_locationDataContext.Object);
        }

        [Test]
        public async Task Then_The_Locations_Are_Returned_That_Start_With_The_Query_Param()
        {
            //Arrange
            var query = "tes";

            //Act
            var actual = await _locationRepository.GetAllStartingWith(query);
        
            //Assert
            actual.Should().BeEquivalentTo(_items.Where(c=>c.LocationName.StartsWith(query)).ToList());
        }     
        
        
        [Test]
        public async Task Then_The_Locations_Are_Returned_That_Start_With_The_Query_Param_And_Limited_By_Count()
        {
            //Arrange
            var query = "tes";
            var results = 1;

            //Act
            var actual = await _locationRepository.GetAllStartingWith(query, results);
        
            //Assert
            actual.Should().BeEquivalentTo(_items.Where(c=>c.LocationName.StartsWith(query)).Take(results).ToList());
        }   
    }
}