using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.Location.Data.UnitTests.Repository.Location
{
    public class WhenGettingByLocationAndAuthorityName
    {
        private Mock<ILocationDataContext> _locationDataContext;
        private List<Domain.Entities.Location> _items;
        private Data.Repository.LocationRepository _locationRepository;
        private Domain.Entities.Location _expectedLocation;

        [SetUp]
        public void Arrange()
        {
            _expectedLocation = new Domain.Entities.Location
            {
                Id = 4,
                LocationName = "different",
                LocalAuthorityName = "test1",
                LocalAuthorityDistrict = "testDistrict"
            };
            _items = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location
                {
                    Id = 1,
                    LocationName = "testing",
                    LocalAuthorityName = "test1",
                    LocalAuthorityDistrict = "testDistrict"
                },
                new Domain.Entities.Location
                {
                    Id = 2,
                    LocationName = "test",
                    LocalAuthorityName = "test1",
                    LocalAuthorityDistrict = "testDistrict"
                },
                new Domain.Entities.Location
                {
                    Id = 3,
                    LocationName = "retest",
                    LocalAuthorityName = "test1",
                    LocalAuthorityDistrict = "testDistrict"
                },
                _expectedLocation
                
            };
        
            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.Locations).ReturnsDbSet(_items);
        

            _locationRepository = new Data.Repository.LocationRepository(_locationDataContext.Object);
        }

        [Test]
        public async Task Then_The_Location_Is_Returned_That_Have_Matching_Name_And_Authority()
        {
            //Act
            var actual = await _locationRepository.GetByLocationAndAuthorityName(_expectedLocation.LocationName, _expectedLocation.LocalAuthorityName);
        
            //Assert
            actual.Should().BeEquivalentTo(_expectedLocation);
        }

        [Test]
        public async Task Then_The_Location_Is_Returned_That_Has_Matching_Name_And_District()
        {
            //Act
            var actual = await _locationRepository.GetByLocationAndAuthorityName(_expectedLocation.LocationName, _expectedLocation.LocalAuthorityDistrict);

            //Assert
            actual.Should().BeEquivalentTo(_expectedLocation);
        }

        [Test]
        public async Task Then_Null_Is_Returned_If_No_Match()
        {
            //Act
            var actual = await _locationRepository.GetByLocationAndAuthorityName("not valid", "not valid");
        
            //Assert
            actual.Should().BeNull();
        }   
    }
}