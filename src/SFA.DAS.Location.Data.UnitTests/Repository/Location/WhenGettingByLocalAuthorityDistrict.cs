using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Data.UnitTests.Repository.Location
{
    public class WhenGettingByLocalAuthorityDistrict
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
                    LocalAuthorityDistrict = "West Midlands"
                },
                new Domain.Entities.Location
                {
                    Id = 1,
                    LocalAuthorityDistrict = "West Midlands"
                },
                new Domain.Entities.Location
                {
                    Id = 1,
                    LocalAuthorityDistrict = "No Match"
                },
                new Domain.Entities.Location
                {
                    Id = 1,
                    LocalAuthorityDistrict = "No Match"
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
            var query = "West Midlands";

            //Act
            var actual = await _locationRepository.GetByLocalAuthorityDistrict(query);

            //Assert
            actual.Should().BeEquivalentTo(_items.Where(c => c.LocalAuthorityDistrict.Equals(query)).ToList());
        }
    }
}
