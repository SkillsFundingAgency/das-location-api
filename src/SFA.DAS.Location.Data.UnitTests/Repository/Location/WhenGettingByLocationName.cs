using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Data.UnitTests.Repository.Location
{
    public class WhenGettingByLocationName
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
                LocalAuthorityName = "test1"
            };
            _items = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location
                {
                    Id = 1,
                    LocationName = "testing",
                    LocalAuthorityName = "test1"
                },
                new Domain.Entities.Location
                {
                    Id = 2,
                    LocationName = "test",
                    LocalAuthorityName = "test1"
                },
                new Domain.Entities.Location
                {
                    Id = 3,
                    LocationName = "retest",
                    LocalAuthorityName = "test1"
                },
                _expectedLocation
            };

            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.Locations).ReturnsDbSet(_items);
            _locationRepository = new Data.Repository.LocationRepository(_locationDataContext.Object);
        }

        [Test]
        public async Task Then_The_Location_Is_Returned_That_Has_Matching_Location_Name_And_Associated_Authority_Name()
        {
            //Act
            var actual = await _locationRepository.GetByLocationName(_expectedLocation.LocationName);

            //Assert
            actual.Should().BeEquivalentTo(_expectedLocation);
            actual.Should().NotBeNull(_expectedLocation.LocalAuthorityName);
        }

        [Test]
        public async Task Then_Location_Is_Returned_That_Matches_Partial_Location_Name_And_Associated_Authority_Name()
        {
            //Act
            var actual = await _locationRepository.GetByLocationName("differ");

            //Assert
            actual.Should().BeEquivalentTo(_expectedLocation);
            actual.Should().NotBeNull(_expectedLocation.LocalAuthorityName);
        }

        [Test]
        public async Task Then_Null_Is_Returned_If_No_Match()
        {
            //Act
            var actual = await _locationRepository.GetByLocationName("not valid");

            //Assert
            actual.Should().BeNull();
        }
    }
}
